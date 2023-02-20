using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Organic.Areas.Admin.ViewModels.Product.Tag;
using Organic.Areas.Admin.ViewModels.Slider;
using Organic.Contracts.File;
using Organic.Database;
using Organic.Services.Abstracts;

namespace Organic.Areas.Admin.ViewComponents
{
    [Area("Admin")]
    [ViewComponent(Name = "SliderTable")]
    public class SliderTable : ViewComponent
    {
        private readonly DataContext _dbContext;
        private readonly IFileService _fileService;

        public SliderTable(DataContext dbContext, IFileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var slider = await _dbContext.Sliders.OrderByDescending(s => s.CreatedAt).Take(4).Select(s => new ListViewModel(
                s.Id,
                s.Product!.Name!,
                s.Title!,
                s.Description!,
                _fileService.GetFileUrl(s.ImageNameInSystem, UploadDirectory.Slider),
                s.CreatedAt)).ToListAsync();

            return View(slider);
        }
    }
}
