using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Organic.Areas.Admin.ViewModels.Product.Discount;
using Organic.Areas.Client.ViewModels.Basket;
using Organic.Areas.Client.ViewModels.Product;
using Organic.Contracts.File;
using Organic.Contracts.ProductImage;
using Organic.Database;
using Organic.Services.Abstracts;
using static Organic.Areas.Client.ViewModels.Product.ListItemViewModel;

namespace Organic.Areas.Client.ViewComponents
{
    [Area("Client")]
    [ViewComponent(Name = "ProductPage")]
    public class ProductPage : ViewComponent
    {
        public readonly DataContext _dataContext;
        public readonly IFileService _fileService;
        public ProductPage(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync(List<ListItemViewModel> searchingProduct = null)
        {
            //if (searchingProduct is not null)
            //{
            //    return View(searchingProduct);
            //}
            var model = await _dataContext.Products
                .Where(p => p.ProductCounts!.Any(pc => pc.Count > 0))
                .OrderByDescending(p => p.CreatedAt)
                .Select(p => new ListItemViewModel(
                        p.Id,
                        p.Name!,
                        p.Info,
                        p.Price,
                        p.ProductImages!.Take(1).FirstOrDefault() != null
                   ? _fileService.GetFileUrl(p.ProductImages!.Take(1).FirstOrDefault()!.ImageNameInFileSystem, UploadDirectory.Product)
                   : Image.DEFAULTIMAGE,
                        p.Category.Name,
                        p.ProductDiscountPercents!.Select(pdp=> new DiscountPercentViewModel(pdp.Percent)).ToList(),
                        p.ProductTags.Select(p => p.Tag).Select(p => new TagViewModel(p.Name)).ToList()))
                        
                .ToListAsync();

            return View(model);
        }
    }
}
