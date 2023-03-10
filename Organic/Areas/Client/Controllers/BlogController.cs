using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Organic.Areas.Admin.ViewModels.Blog;
using Organic.Areas.Admin.ViewModels.Product.Count;
using Organic.Areas.Admin.ViewModels.Product.Discount;
using Organic.Areas.Admin.ViewModels.Product.Image;
using Organic.Areas.Admin.ViewModels.Product.Tag;
using Organic.Areas.Client.ViewModels.Blog;
using Organic.Areas.Client.ViewModels.Product;
using Organic.Contracts.File;
using Organic.Contracts.ProductImage;
using Organic.Database;
using Organic.Services.Abstracts;

namespace Organic.Areas.Client.Controllers
{
    [Area("client")]
    [Route("client/blog")]
    public class BlogController : Controller
    {
        private readonly DataContext _dbContext;
        private readonly IFileService _fileService;
        public BlogController(DataContext dbContext, IFileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
        }

        [HttpGet("list", Name = "client-blog-list")]
        public async Task<IActionResult> ListAsync([FromServices] IFileService fileService)
        {
            var model = await _dbContext.Blogs
                .Select(b => new BlogViewModel(b.Id,
                b.Title,
                b.Description!.Substring(0, 50),
                b.From!.FirstName,
                b.PostedDate,
                b.ImageNameInSystem!.FirstOrDefault() != null
                ? _fileService.GetFileUrl(b.ImageNameInSystem, UploadDirectory.Blog)
                : Image.DEFAULTIMAGE,
                b.BlogAndCategories!.Select(b => b.BlogCategory)
                .Select(b => new BlogCategoryViewModel(b!.Id, b.Name!)).ToList(),
                 b.Likes!.Count,
                 b.Comments!.Count))
                .ToListAsync();

            return View(model);
        }

        [HttpGet("blog/{id}", Name = "client-blog-single")]
        public async Task<IActionResult> Detail(int id)
        {
            var blog = await _dbContext.Blogs
                .Include(p => p.Likes)
                .Include(p => p.Comments)
                .Include(p=> p.From)
                .Include(p=> p.BlogAndCategories)
     .FirstOrDefaultAsync(p => p.Id == id);

            if (blog is null)
            {
                return NotFound();
            }
            //var category = _dbContext.BlogCategories!.Select(bc => bc.BlogCategory)
            //   .FirstOrDefault(b=>b.Id)

            //var imageNameİnSystem = _fileService.GetFileUrl(blog.ImageNameInSystem, UploadDirectory.Blog);

            //var viewModel = new BlogDetailViewModel(
            //   blog.Id,
            //   blog.Title!,
            //   blog.Description!,
            //   blog.PostedDate,
            //   imageNameİnSystem,
            //   blog.From!.FirstName!,
            //   category
              
            //   );

            

            return View(blog);
        }
    }
}
