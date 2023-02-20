using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Organic.Areas.Admin.ViewModels.Product;
using Organic.Areas.Admin.ViewModels.Slider;
using Organic.Contracts.File;
using Organic.Database;
using Organic.Services.Abstracts;

namespace Organic.Areas.Client.ViewComponents
{
    [Area("Client")]
    [ViewComponent(Name = "Slider")]
    public class Slider : ViewComponent
    {
        public readonly DataContext _dataContext;
        public readonly IFileService _fileService;
        public Slider(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _dataContext.Sliders
                .Select(b => new IndexViewModel(
                        b.Id,
                        b.ProductId,
                        b.Product!.Name,
                        b.Title,
                        b.Description,
                        _fileService.GetFileUrl(b.ImageNameInSystem, UploadDirectory.Slider))).ToListAsync();

            return View(model);
        }
    }
}
