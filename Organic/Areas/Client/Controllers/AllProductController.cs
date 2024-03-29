﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Organic.Areas.Admin.ViewModels.Product.Count;
using Organic.Areas.Admin.ViewModels.Product.Discount;
using Organic.Areas.Admin.ViewModels.Product.Tag;
using Organic.Areas.Client.ViewComponents;
using Organic.Areas.Client.ViewModels.Product;
using Organic.Contracts.File;
using Organic.Database;
using Organic.Database.Models;
using Organic.Services.Abstracts;
using Product = Organic.Database.Models.Product;

namespace Organic.Areas.Client.Controllers
{
    [Area("client")]
    [Route("client/product")]
    public class AllProductController : Controller
    {
        private readonly DataContext _dbContext;
        private readonly IFileService _fileService;
        public AllProductController(DataContext dbContext, IFileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
        }

        #region Rate

        [HttpGet("Rate/{id}",Name ="product-rate")]
        public ActionResult Rate(int id)
        {
            var product = _dbContext.Products.Find(id);

            if (product is null) return NotFound();

                product.RatingCount++;
                _dbContext.SaveChanges();
            
            return RedirectToRoute("client-product-detail", new { id = product!.Id });
        }
        #endregion

    }
}
