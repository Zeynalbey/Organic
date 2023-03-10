using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Organic.Areas.Admin.ViewModels.Product;
using Organic.Areas.Admin.ViewModels.Product.Count;
using Organic.Areas.Admin.ViewModels.Product.Discount;
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
                .Select(p => new ListItemViewModel(p.Id, p.Name, p.Info, p.Rating, p.RatingCount, p.Price, p.CreatedAt, p.Category!.Id, p.Category.Name!,
                 p.ProductCounts!
                .Select(pc => new CountViewModel(pc.Id, pc.Count)).ToList(),
                 p.ProductDiscountPercents!
                 .Select(pc => new DiscountViewModel(pc.Id, pc.Percent)).ToList()))                 
                .ToListAsync();

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

            foreach (var tagId in model.TagIds!)
            {
                if (!_dbContext.Tags.Any(t => t.Id == tagId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Category with id({tagId}) not found in db ");
                    return GetView(model);
                }
            }
            if (!_dbContext.Categories.Any(a => a.Id == model.CategoryId))
            {
                ModelState.AddModelError(string.Empty, "Category is not found");
                return GetView(model);
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
                    CategoryID = model.CategoryId
                };

                _dbContext.Products.Add(product);
                _dbContext.SaveChanges();


                var productCount = new ProductCount
                {
                    Id = product.Id,
                    Count = model.Count,
                };

                _dbContext.ProductCounts.Add(productCount);

                var discountPercent = new ProductDiscountPercent
                {
                    Id = product.Id,
                    Percent = model.Percent
                };

                _dbContext.ProductDiscountPercents!.Add(discountPercent);

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

        #region Update

        [HttpGet("update/{id}", Name = "admin-product-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var product = await _dbContext.Products
                .Include(b => b.ProductTags).FirstOrDefaultAsync(b=>b.Id == id);

            var productCount = await _dbContext.ProductCounts.FirstAsync(p => p.Id == product!.Id);
            var productDiscountPercent = await _dbContext.ProductDiscountPercents!.FirstOrDefaultAsync(p => p.Id == product!.Id);

            if (product is null)
            {
                return NotFound();
            }

            if (productDiscountPercent == null)
            {
                return NotFound();
            }

            var model = new UpdateProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Info = product.Info,
                Count = productCount.Count,
                Percent = productDiscountPercent.Percent,
                CategoryId = product.CategoryID,
                Categories = await _dbContext.Categories.Select(c => new CategoryListViewModel(c.Id, c.Name)).ToListAsync(),
                Tags = await _dbContext.Tags.Select(c => new TagListViewModel(c.Id, c.Name)).ToListAsync(),
                TagIds = product.ProductTags!.Select(pt => pt.TagId).ToList()
            };

            return View(model);
        }

        [HttpPost("update/{id}", Name = "admin-product-update")]
        public async Task<IActionResult> UpdateAsync(UpdateProductViewModel model)
        {
            var product = await _dbContext.Products
                .Include(p => p.ProductTags).FirstOrDefaultAsync(p => p.Id == model.Id);

            var productCount = await _dbContext.ProductCounts.FirstAsync(p => p.Id == product!.Id);
            var productDiscountPercent = await _dbContext.ProductDiscountPercents!.FirstAsync(p => p.Id == product!.Id);

            #region Errors

            if (product is null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return GetView(model);
            }

            foreach (var id in model.TagIds!)
            {
                if (!await _dbContext.Tags.AnyAsync(t => t.Id == id))
                {
                    ModelState.AddModelError(String.Empty, "Something went wrong");
                    return GetView(model);
                }
            }

            #endregion

            #region GetView

            IActionResult GetView(UpdateProductViewModel model)
            {
                model.Categories = _dbContext.Categories
                    .Select(c => new CategoryListViewModel(c.Id, c.Name)).ToList();
                model.Tags = _dbContext.Tags
                    .Select(c => new TagListViewModel(c.Id, c.Name)).ToList();

                model.CategoryId = product.CategoryID;
                model.TagIds = product.ProductTags!.Select(c => c.TagId).ToList();

                return View(model);
            }
            #endregion

            var productId = product.Id;
            var count = model.Count;
            product.Name = model.Name;
            product.Info = model.Info;
            product.Price = model.Price;
            product.CategoryID = model.CategoryId;
            productCount.Count = model.Count;
            productDiscountPercent.Percent = model.Percent;

            var DbTag = product.ProductTags!.Select(bc => bc.TagId).ToList();
            var RemoveTag = DbTag.Except(model.TagIds).ToList();
            var AddTag = model.TagIds.Except(DbTag).ToList();

            product.ProductTags!.RemoveAll(bc => RemoveTag.Contains(bc.TagId));

            foreach (var tagId in AddTag)
            {
                var productTag = new ProductTag
                {
                    TagId = tagId,
                    Product = product,
                };

                await _dbContext.ProductTags.AddAsync(productTag);
            }

            await _dbContext.SaveChangesAsync();

            return RedirectToRoute("admin-product-list");
        }

        #endregion

        #region Delete

        [HttpPost("delete/{id}", Name = "admin-product-delete")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(b => b.Id == id);
            if (product is null)
            {
                return NotFound();
            }

            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();

            return RedirectToRoute("admin-product-list");
        }

        #endregion

    }
}