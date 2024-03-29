﻿using Organic.Areas.Client.ViewModels.Basket;
using Organic.Database;
using Organic.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Xml.Linq;
using Organic.Database.Models;
using System.Linq;
using Organic.Contracts.ProductImage;

namespace Organic.Areas.Client.ViewComponents
{
    [ViewComponent(Name = "BasketMini")]

    public class BasketMini : ViewComponent
    {

        private readonly DataContext _dataContext;
        private readonly IUserService _userService;
        private readonly IFileService _fileService;

        public BasketMini(DataContext dataContext, IUserService userService = null, IFileService fileService = null)
        {
            _dataContext = dataContext;
            _userService = userService;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync(List<ProductCookieViewModel>? viewModels = null)
        {
            if (_userService.IsAuthenticated)
            {
                var model = await _dataContext.BasketProducts
                    .Include(p => p.Product)
                        .ThenInclude(p => p!.ProductDiscountPercents).Where(p => p.Basket!.UserId == _userService.CurrentUser.Id)
                   .Select(p =>
                   new ProductCookieViewModel(p.ProductId, p.Product!.Name,
                   p.Product.ProductImages!.Take(1).FirstOrDefault()! != null
                   ? _fileService.GetFileUrl(p.Product.ProductImages!.Take(1).FirstOrDefault()!.ImageNameInFileSystem, Contracts.File.UploadDirectory.Product)
                   : Image.DEFAULTIMAGE,
                   p.Quantity, p.Product.Price, (100 - p.Product.ProductDiscountPercents!.FirstOrDefault()!.Percent) / 100 * p.Product.Price,
                   (100 - p.Product.ProductDiscountPercents!.Take(1).FirstOrDefault()!.Percent) / 100 * p.Product.Price * p.Quantity)
                   ).ToListAsync();

                return View(model);
            }

            if (viewModels is not null)
            {
                return View(viewModels);
            }

            var productsCookieValue = HttpContext.Request.Cookies["products"];
            var productsCookieViewModel = new List<ProductCookieViewModel>();
            if (productsCookieValue is not null)
            {
                productsCookieViewModel = JsonSerializer.Deserialize<List<ProductCookieViewModel>>(productsCookieValue);
            }

            return View(productsCookieViewModel);
        }
    }
}
