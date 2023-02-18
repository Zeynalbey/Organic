using Organic.Areas.Admin.ViewModels;
using Organic.Contracts.File;
using Organic.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Organic.Areas.Admin.ViewModels.Product.Tag;

namespace Organic.Areas.Admin.ViewComponents
{
    [Area("Admin")]
    [ViewComponent(Name = "TagTable")]
    public class TagTable : ViewComponent
    {
        private readonly DataContext _dbContext;

        public TagTable(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var tag = await _dbContext.Tags.OrderByDescending(c => c.CreatedAt).Take(4).Select(c => new TagViewModel(
                c.Id,
                c.Name!
                )).ToListAsync();

            return View(tag);
        }
    }
}
