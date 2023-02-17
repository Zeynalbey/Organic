using Organic.Areas.Admin.ViewModels;
using Organic.Contracts.File;
using Organic.Database;
using Organic.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Organic.Areas.Admin.ViewModels.Product.Category;

namespace Organic.Areas.Admin.ViewComponents
{
    [Area("Admin")]
    [ViewComponent(Name = "CategoryTable")]
    public class CategoryTable : ViewComponent
    {
        private readonly DataContext _dbContext;

        public CategoryTable(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var category = await _dbContext.Categories.OrderByDescending(c => c.CreatedAt).Take(4).Select(c => new CategoryViewModel(
                c.Id,
                c.Name!,
                c.IconClass!
                )).ToListAsync();

            return View(category);
        }
    }
}
