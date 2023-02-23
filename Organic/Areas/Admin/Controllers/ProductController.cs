using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Organic.Areas.Admin.ViewModels.Product;
using Organic.Areas.Admin.ViewModels.Product.Count;
using Organic.Areas.Admin.ViewModels.Product.Rate;
using Organic.Database;
using Organic.Database.Models;
using Organic.Services.Abstracts;

namespace Organic.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/product")]
    [Authorize(Roles = "admin, moderator")]
    public class ProductController : Controller
    {
        private readonly DataContext _dbContext;
        private readonly IFileService _fileService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(DataContext dbContext, IFileService fileService, ILogger<ProductController> logger)
        {
            _dbContext = dbContext;
            _fileService = fileService;
            _logger = logger;
        }

        #region List

        [HttpGet("list", Name = "admin-product-list")]
        public async Task<IActionResult> ListAsync()
        {
            var product = await _dbContext.Products
                .Select(p => new ListItemViewModel(p.Id, p.Name, p.Info, p.Price, p.CreatedAt, p.Category!.Id, p.Category.Name!, p.ProductRates!
                .Select(r => new RateViewModel(r.Id, r.Rating)).ToList(),
                 p.ProductCounts!
                .Select(pc => new CountViewModel(pc.Id, pc.Count)).ToList())).ToListAsync();

            return View(product);
        }
        #endregion

        #region Add

        [HttpGet("add", Name = "admin-product-add")]
        public async Task<IActionResult> Add()
        {
            var product = new AddProductViewModel
            {
                Categories = await _dbContext.Categories.Select(c => new CategoryListViewModel
                (c.Id, c.Name)).ToListAsync(),
                Tags = await _dbContext.Tags.Select(t => new TagListViewModel(t.Id, t.Name)).ToListAsync()
            };

            return View(product);
        }

        [HttpPost("add", Name = "admin-product-add")]
        public async Task<IActionResult> Add(AddProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return GetView(model);
            }


            foreach (var categoryId in model.CategoryIds!)
            {
                if (!_dbContext.Categories.Any(c => c.Id == categoryId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Category with id({categoryId}) not found in db ");
                    return GetView(model);
                }

            }

            foreach (var tagId in model.TagIds!)
            {
                if (!_dbContext.Tags.Any(t => t.Id == tagId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Category with id({tagId}) not found in db ");
                    return GetView(model);
                }
            }

            AddProduct();

            await _dbContext.SaveChangesAsync();

            return RedirectToRoute("admin-product-list");

            IActionResult GetView(AddProductViewModel model)
            {
                model.Categories = _dbContext.Categories
                   .Select(c => new CategoryListViewModel(c.Id, c.Name))
                   .ToList();

                model.Tags = _dbContext.Tags
                   .Select(c => new TagListViewModel(c.Id, c.Name))
                   .ToList();

                return View(model);
            }


            Product AddProduct()
            {
                var product = new Product
                {
                    Name = model.Name,
                    Info = model.Info!,
                    Price = model.Price,
                    CategoryID = model.CategoryIds.FirstOrDefault(),
                };

                _dbContext.Products.Add(product);
                _dbContext.SaveChanges();


                var productCount = new ProductCount
                {
                    Id = product.Id,
                    Count = model.Count
                };

                _dbContext.ProductCounts.Add(productCount);


                foreach (var tagId in model.TagIds)
                {
                    var ProductTag = new ProductTag
                    {
                        TagId = tagId,

                        Product = product,
                    };

                    _dbContext.ProductTags.Add(ProductTag);
                }

                return product;
            }

        }

        #endregion



    }
}