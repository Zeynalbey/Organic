using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Organic.Areas.Client.ViewModels.Category;
using Organic.Database;
using Organic.Services.Abstracts;

namespace Organic.Areas.Client.ViewComponents
{
    [Area("Client")]
    [ViewComponent(Name = "Category")]
    public class Category : ViewComponent
    {
        public readonly DataContext _dataContext;
        public readonly IFileService _fileService;
        public Category(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _dataContext.Categories
                .Select(b => new CategoryViewModel(
                        b.Id,
                        b.Name!,
                        b.IconClass,
                        b.Products!.
                        Select(p=> new ViewModels.Product.ProductViewModel(p.Id, p.Name, p.Info, p.Price)).ToList()))
                .ToListAsync();

            return View(model);
        }
    }
}
