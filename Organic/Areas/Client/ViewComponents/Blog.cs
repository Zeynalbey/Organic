using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Organic.Areas.Admin.ViewModels.Blog;
using Organic.Areas.Client.ViewModels.Blog;
using Organic.Contracts.File;
using Organic.Contracts.ProductImage;
using Organic.Database;
using Organic.Services.Abstracts;
using Organic.Services.Concretes;

namespace Organic.Areas.Client.ViewComponents
{
    [Area("Client")]
    [ViewComponent(Name = "Blog")]
    public class Blog : ViewComponent
    {
        public readonly DataContext _dataContext;
        public readonly IFileService _fileService;
        public Blog(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _dataContext.Blogs
                .Select(b => new BlogViewModel(b.Id,
                b.Title,
                b.Description!.Substring(0,40),
                b.From!.FirstName, 
                b.PostedDate,
                b.ImageNameInSystem!.FirstOrDefault() !=null
                ? _fileService.GetFileUrl(b.ImageNameInSystem, UploadDirectory.Blog)
                : Image.DEFAULTIMAGE,
                b.BlogAndCategories!.Select(b => b.BlogCategory)
                .Select(b => new BlogCategoryViewModel(b!.Id, b.Name!)).ToList(),
                 b.Likes!.Count,
                 b.Comments!.Count))
                .ToListAsync();

            return View(model);           
        }
    }
}

