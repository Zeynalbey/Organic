using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Organic.Database;
using Organic.Services.Abstracts;
using Organic.Areas.Client.ViewModels.Category;

namespace Organic.Areas.Client.ViewComponents
{
    [ViewComponent(Name = "ShopPageCategory")]
    public class ShopPageCategory : ViewComponent
    {

        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        public ShopPageCategory(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            
           var  model = await _dataContext.Categories.Select(c => new CategoryItemViewModel(c.Id, c.Name!)).ToListAsync();
            
             return View(model);
        }
    }

}
