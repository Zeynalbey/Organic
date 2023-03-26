using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Organic.Areas.Admin.ViewModels.Product.Discount;
using Organic.Areas.Client.ViewModels.Product;
using Organic.Contracts.File;
using Organic.Contracts.ProductCategory;
using Organic.Contracts.ProductImage;
using Organic.Database;
using Organic.Services.Abstracts;

namespace Organic.Areas.Client.ViewComponents
{
    [Area("Client")]
    [ViewComponent(Name = "Product")]
    public class Product : ViewComponent
    {
        public readonly DataContext _dataContext;
        public readonly IFileService _fileService;
        public Product(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            
            var model = await _dataContext.Products
                .Where(p => p.ProductCounts!.Any(pc => pc.Count > 0))
                .OrderByDescending(p => p.CreatedAt)
                .Select(p => new ProductSaleViewModel(
                        p.Id,
                        p.Name!,
                        p.Info,
                        p.Category!.Name!,
                        p.Rating,
                        p.RatingCount,
                        p.Price!,
                        p.ProductDiscountPercents!.Select(pdp=> new DiscountViewModel(pdp.Id, pdp.Percent)).ToList(),
                        p.ProductImages!.Take(1).FirstOrDefault() != null
                   ? _fileService.GetFileUrl(p.ProductImages!.Take(1).FirstOrDefault()!.ImageNameInFileSystem, UploadDirectory.Product)
                   : Image.DEFAULTIMAGE))
                .ToListAsync();

            return View(model);
        }
    }
}
