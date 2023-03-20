using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Organic.Areas.Admin.ViewModels.Product.Discount;
using Organic.Areas.Client.ViewModels.Product;
using Organic.Contracts.Email;
using Organic.Contracts.File;
using Organic.Contracts.ProductImage;
using Organic.Database;
using Organic.Services.Abstracts;

namespace Organic.Areas.Client.ViewComponents
{
    [Area("Client")]
    [ViewComponent(Name = "RatingProduct")]
    public class RatingProduct : ViewComponent
    {
        public readonly DataContext _dataContext;
        public readonly IFileService _fileService;
        public RatingProduct(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _dataContext.Products.OrderByDescending(p => p.RatingCount)
               .Select(p => new ProductSaleViewModel(
                       p.Id,
                       p.Name!,
                       p.Info,
                       p.Category!.Name!,
                       p.Rating,
                       p.RatingCount,
                       p.Price!,
                       p.ProductDiscountPercents!.Select(pdp => new DiscountViewModel(pdp.Id, pdp.Percent)).ToList(),
                       p.ProductImages!.Take(1).FirstOrDefault() != null
                  ? _fileService.GetFileUrl(p.ProductImages!.Take(1).FirstOrDefault()!.ImageNameInFileSystem, UploadDirectory.Product)
                  : Image.DEFAULTIMAGE))
               .ToListAsync();

            return View(model);
        }
    }
}
