﻿using Organic.Areas.Client.ViewModels.Basket;
using Organic.Database;
using Organic.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Xml.Linq;
using Organic.Contracts.File;
using Organic.Contracts.ProductImage;

namespace Backend_Final.Areas.Client.ViewComponents
{
    [ViewComponent(Name = "CardPage")]

    public class CardPage : ViewComponent
    {
        private readonly DataContext _dataContext;
        private readonly IUserService _userService;
        private readonly IFileService _fileService;

        public CardPage(DataContext dataContext, IUserService userService = null, IFileService fileService = null)
        {
            _dataContext = dataContext;
            _userService = userService;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync(List<ProductCookieViewModel>? viewModels = null)
        {
            //var discountPrice = await _dataContext.ProductDiscountPercents!.Include(p => p.Product).FirstOrDefaultAsync(p => p.Id == p.Product!.Id);

            if (_userService.IsAuthenticated)
            {
                var model = await _dataContext.BasketProducts
                    .Where(p => p.Basket!.UserId == _userService.CurrentUser.Id)
                    .Select(p => new ProductCookieViewModel(p.ProductId, p.Product!.Name,
                    p.Product!.ProductImages!.Take(1).FirstOrDefault()! != null
                    ? _fileService.GetFileUrl(p.Product!.ProductImages!.Take(1).FirstOrDefault()!.ImageNameInFileSystem, UploadDirectory.Product)
                    : Image.DEFAULTIMAGE,
                    p.Quantity, p.Product.Price,
                    Math.Round((100 - p.Product.ProductDiscountPercents.FirstOrDefault().Percent) / 100 * p.Product.Price, 2),
                    Math.Round((100 - p.Product.ProductDiscountPercents.FirstOrDefault().Percent) / 100 * p.Product.Price, 2) * p.Quantity))
                    .ToListAsync();



                return View(model);
            }

            if (viewModels is not null) return View(viewModels);


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
