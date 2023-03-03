using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Organic.Areas.Admin.ViewModels.Product.Count;
using Organic.Areas.Admin.ViewModels.Product;
using Organic.Areas.Admin.ViewModels.Product.Tag;
using Organic.Areas.Admin.ViewModels.Slider;
using Organic.Contracts.File;
using Organic.Database;
using Organic.Services.Abstracts;
using Organic.Areas.Admin.ViewModels.Product.Discount;

namespace Organic.Areas.Admin.ViewComponents
{
    [Area("Admin")]
    [ViewComponent(Name = "ProductTable")]
    public class ProductTable : ViewComponent
    {
        private readonly DataContext _dbContext;
        private readonly IFileService _fileService;

        public ProductTable(DataContext dbContext, IFileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var product = await _dbContext.Products
                .Select(p => new ListItemViewModel(p.Id, p.Name, p.Info, p.Rating, p.RatingCount, p.Price, p.CreatedAt, p.Category!.Id, p.Category.Name!,
                 p.ProductCounts!
                .Select(pc => new CountViewModel(pc.Id, pc.Count)).ToList(),
                 p.ProductDiscountPercents!.Select(pd => new DiscountViewModel(pd.Id, pd.Percent)).ToList())).ToListAsync();

            return View(product);
        }
    }
}
