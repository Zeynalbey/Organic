﻿using Organic.Contracts.Identity;
using Organic.Database;
using Organic.Database.Models;
using Organic.Exceptions;
using Organic.Services.Abstracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.Json;
using Organic.Areas.Client.ViewModels.Authentication;
using Organic.Areas.Client.ViewModels.Basket;
using Microsoft.AspNetCore.Identity;
using System;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Security;
using Organic.Contracts.File;
using Organic.Contracts.ProductImage;

namespace Organic.Services.Concretes
{
    public class UserService : IUserService
    {
        private readonly DataContext _dataContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserActivationService _userActivationService;
        private readonly IFileService _fileService;
        private User _currentUser;

        public UserService(
            DataContext dataContext,
            IHttpContextAccessor httpContextAccessor,
            IUserActivationService userActivationService,
            IFileService fileService)
        {
            _dataContext = dataContext;
            _httpContextAccessor = httpContextAccessor;
            _userActivationService = userActivationService;
            _fileService = fileService;
        }

        public bool IsAuthenticated
        {
            get => _httpContextAccessor.HttpContext!.User.Identity!.IsAuthenticated;
        }

        public User CurrentUser
        {
            get
            {
                if (_currentUser is not null)
                {
                    return _currentUser;
                }

                var idClaim = _httpContextAccessor.HttpContext!.User.Claims.FirstOrDefault(C => C.Type == CustomClaimNames.ID);
                if (idClaim is null)
                    throw new IdentityCookieException("Identity cookie not found");

                _currentUser = _dataContext.Users.First(u => u.Id == Guid.Parse(idClaim.Value));

                return _currentUser;
            }
        }

        public async Task<bool> CheckEmailConfirmedAsync(string? email)
        {
            return await _dataContext.Users.AnyAsync(u => u.Email == email && u.IsEmailConfirmed);
        }

        public string GetCurrentUserFullName()
        {
            return $"{CurrentUser.FirstName} {CurrentUser.LastName}";
        }

        public string GetCurrentUserRollName()
        {
            var _currentUserRole = _dataContext.Roles.First(u => u.Id == CurrentUser.RoleId);
            return _currentUserRole.Name!;
        }

        public string GetCurrentUserImage()
        {
            var image = _fileService.GetFileUrl(_currentUser.ImageNameInSystem, UploadDirectory.User) != null
                ? _fileService.GetFileUrl(_currentUser.ImageNameInSystem, UploadDirectory.User)
                : String.Empty;
                 return image;
        }

        public async Task<bool> CheckPasswordAsync(string? email, string? password)
        {
            return await _dataContext.Users.AnyAsync(u => u.Email == email && u.Password == password);
        }

        public async Task SignInAsync(Guid id, string? role = null)
        {
            var claims = new List<Claim>
            {
                new Claim(CustomClaimNames.ID, id.ToString())
            };

            if (role is not null)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var userPrincipal = new ClaimsPrincipal(identity);

            await _httpContextAccessor.HttpContext!.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal);
        }

        public async Task SignInAsync(string? email, string? password, string? role = null)
        {
            var user = await _dataContext.Users.FirstAsync(u => u.Email == email && u.Password == password);
            await SignInAsync(user.Id, role);
        }


        public async Task SignOutAsync()
        {
            await _httpContextAccessor.HttpContext!.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public async Task <bool> CheckEmail(string email)
        {
             return await _dataContext.Users.AnyAsync(u => u.Email == email);
        }

        public async Task CreateAsync(RegisterViewModel model)
        {
            var user = await CreateUserAsync();
            var basket = await CreateBasketAsync();
            await CreteBasketProductsAsync();
            await _userActivationService.SendActivationUrlAsync(user);

            await _dataContext.SaveChangesAsync();

            async Task<User> CreateUserAsync()
            {
                var user = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Password = model.Password,
                    RoleId = 3
                };
                await _dataContext.Users.AddAsync(user);

                return user;
            }

            async Task<Basket> CreateBasketAsync()
            {
                //Create basket process
                var basket = new Basket
                {
                    User = user,
                };
                await _dataContext.Baskets.AddAsync(basket);

                return basket;
            }

            async Task CreteBasketProductsAsync()
            {
                //Add products to basket if cookie exists
                var productCookieValue = _httpContextAccessor.HttpContext!.Request.Cookies["products"];
                if (productCookieValue is not null)
                {
                    var productsCookieViewModel = JsonSerializer.Deserialize<List<ProductCookieViewModel>>(productCookieValue);
                    foreach (var productCookieViewModel in productsCookieViewModel!)
                    {
                        var product = await _dataContext.Products.FirstOrDefaultAsync(b => b.Id == productCookieViewModel.Id);
                        var basketProduct = new BasketProduct
                        {
                            Basket = basket,
                            ProductId = product!.Id,
                            Quantity = productCookieViewModel.Quantity,
                        };

                        await _dataContext.BasketProducts.AddAsync(basketProduct);
                    }
                }
            }

        }

        public async Task<bool> CheckEmailandUserAsync(string? email)
        {
            if (await _dataContext.Users.AnyAsync(u => u.Email == email && u.IsEmailConfirmed))
            {
                await ForgotPasswordUser(email);
                return true;
            }

            async Task ForgotPasswordUser(string email)
            {
                var user = await _dataContext.Users.FirstAsync(u => u.Email == email);
                await _userActivationService.SendActivationUrlAsync(user);
            }

            return false;
        }


    }


}
