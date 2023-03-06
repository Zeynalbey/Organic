using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Organic.Areas.Admin.ViewModels.Product.Count;
using Organic.Areas.Admin.ViewModels.Product.Discount;
using Organic.Areas.Admin.ViewModels.Product.Tag;
using Organic.Areas.Client.ViewModels.Product;
using Organic.Contracts.File;
using Organic.Database;
using Organic.Services.Abstracts;

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

        [HttpGet("list", Name = "client-product-list")]
        public async Task<IActionResult> ListAsync([FromServices] IFileService fileService)
        {
            return View();
        }

        [HttpGet("Rate/{id}/{rate}")]
        public ActionResult Rate(int id, int rate)
        {
            var product = _dbContext.Products.Find(id);
            if (product != null)
            {
                product.Rating += rate;
                product.RatingCount++;
                _dbContext.SaveChanges();
            }
            return RedirectToRoute("client-home-index");
        }

        [HttpGet("detail/{id}", Name = "client-product-detail")]
        public async Task<IActionResult> Detail(int id)
        {

            var product = await _dbContext.Products.Include(p => p.ProductImages)
                .Include(p => p.ProductTags)
                .Include(p => p.ProductDiscountPercents)
                .Include(p => p.ProductCounts)
     .FirstOrDefaultAsync(p => p.Id == id);

            if (product is null)
            {
                return NotFound();
            }

            var imageUrls = product.ProductImages!.Select(pi => new ProductImageViewModel
            {
                Id = pi.Id,
                ImageUrl = _fileService.GetFileUrl(pi.ImageNameInFileSystem, UploadDirectory.Product)
            });

            var viewModel = new ProductDetailViewModel(
                product.Id,
                product.Name!,
                product.Info!,
                product.Rating,
                product.RatingCount,
                product.Price,
                product.ProductDiscountPercents!.Select(pdp => new DiscountViewModel(pdp.Id, pdp.Percent)) ?? new List<DiscountViewModel>(),
                imageUrls,
                product.ProductTags!.Select(pt => pt.Tag).Select(t => new TagViewModel(t.Id, t.Name!)) ?? new List<TagViewModel>(),
                product.ProductCounts!.Select(pc => new CountViewModel(pc.Id, pc.Count)).ToList() ?? new List<CountViewModel>());

            return View(viewModel);
        }
    }
}
