using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Organic.Areas.Admin.ViewModels.Product.Discount;
using Organic.Areas.Client.ViewModels.Product;
using Organic.Contracts.File;
using Organic.Contracts.Product;
using Organic.Contracts.ProductCategory;
using Organic.Contracts.ProductImage;
using Organic.Database;
using Organic.Services.Abstracts;
using static Organic.Areas.Client.ViewModels.Product.ListItemViewModel;
using Image = Organic.Contracts.ProductImage.Image;

namespace Organic.Areas.Client.Controllers
{
    [Area("client")]
    [Route("client/product/page")]
    public class ProductPageController : Controller
    {
        private readonly DataContext _dbContext;
        private readonly IFileService _fileService;
        public ProductPageController(DataContext dbContext, IFileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
        }

        [HttpGet("list", Name = "client-product-list")]
        public async Task<IActionResult> Index(string searchBy, string search,
        [FromQuery] int? discountId, 
        [FromQuery] int? imageId, 
        [FromQuery] int? tagId, 
        [FromQuery] int? categoryId,
        [FromQuery] int? startPrice,
        int page = 1)
        {
            var productsQuery = _dbContext.Products.AsQueryable();

            if (searchBy == "Name")
            {
                productsQuery = productsQuery.Where(p => p.Name!.StartsWith(search)
                || Convert.ToString(p.Price).StartsWith(search)
                //|| Convert.ToString(p.SaleCount).StartsWith(search) 
                || search == null);
            }
            else if (discountId is not null || imageId is not null || tagId is not null || categoryId is not null)
            {
                productsQuery = productsQuery.Include(p => p.ProductDiscountPercents)
                    .Include(p => p.ProductImages)
                    .Include(p => p.ProductTags)
                    .Where(p => discountId == null || p.ProductDiscountPercents!.Any(pc => pc.Id == discountId))
                    .Where(p => imageId == null || p.ProductImages!.Any(pc => pc.Id == imageId))
                    .Where(p => tagId == null || p.ProductTags!.Any(pt => pt.TagId == tagId))
                    .Where(p => categoryId == null || p.Category!.Id == categoryId);
            }
            else if(startPrice == Price.LOW)
            {
                productsQuery = productsQuery.Where(p =>p.Price < 20);
            }
            else if (startPrice == Price.MEDIUM)
            {
                productsQuery = productsQuery.Where(p => p.Price >= 20 && p.Price < 35);
            }
            else if (startPrice == Price.HİGH)
            {
                productsQuery = productsQuery.Where(p => p.Price >= 35);
            }

            var product = await productsQuery.Where(p => p.ProductCounts!.Any(pc => pc.Count > 0))
                .Where(p => p.Category!.Name != SelectedCategoryName.Kabab).ToListAsync();

            var newProduct = await productsQuery
                .Where(p => p.ProductCounts!.Any(pc => pc.Count > 0))
                .Where(p => p.Category!.Name != SelectedCategoryName.Kabab)
                //.OrderBy(p=> p.Price)
                .Skip((page-1) * 12).Take(12)
                .Select(p => new ListItemViewModel(p.Id, p.Name!, p.Info!, p.Price,
                               p.ProductImages!.Take(1).FirstOrDefault() != null
                               ? _fileService.GetFileUrl(p.ProductImages!.Take(1).FirstOrDefault()!.ImageNameInFileSystem, UploadDirectory.Product)
                               : Image.DEFAULTIMAGE,
                                p.Category!.Name!,
                                p.ProductDiscountPercents!.Select(p => new DiscountPercentViewModel(p.Percent)).ToList(),
                                p.ProductTags!.Select(p => p.Tag).Select(p => new TagViewModel(p!.Name!)).ToList()
                                )).ToListAsync();


            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = Math.Ceiling((decimal)product.Count() / 12);

            return View(newProduct);
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
                ImageUrl =  _fileService.GetFileUrl(pi.ImageNameInFileSystem, UploadDirectory.Product)
                
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
                product.ProductTags!.Select(pt => pt.Tag).Select(t => new Admin.ViewModels.Product.Tag.TagViewModel(t.Id, t.Name!)) ?? new List<Admin.ViewModels.Product.Tag.TagViewModel>(),
                product.ProductCounts!.Select(pc => new Admin.ViewModels.Product.Count.CountViewModel(pc.Id, pc.Count)).ToList() ?? new List<Admin.ViewModels.Product.Count.CountViewModel>());

            return View(viewModel);
        }
    }


}
