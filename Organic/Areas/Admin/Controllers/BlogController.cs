using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Organic.Contracts.File;
using Organic.Database;
using Organic.Services.Abstracts;
using Organic.Areas.Admin.ViewModels.Blog;
using Organic.Database.Models;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Organic.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/blog")]
    [Authorize(Roles = "admin,moderator")]
    public class BlogController : Controller
    {
        private readonly DataContext _dbContext;
        private readonly IFileService _fileService;
        private readonly IUserService _userService;
        public readonly ILogger<BlogController> _logger;

        public BlogController(DataContext dbContext,
            IFileService fileService,
            IUserService userService,
            ILogger<BlogController> logger)
        {
            _dbContext = dbContext;
            _fileService = fileService;
            _userService = userService;
            _logger = logger;
        }

        #region List
        [HttpGet("list", Name = "admin-blog-list")]
        public async Task<IActionResult> ListAsync()
        {
            var blog = await _dbContext.Blogs.Select(b => new BlogListViewModel(
             b.Id,
             b.Title,
             b.Description,
             $"{b.From!.FirstName} {b.From.LastName}",
             b.PostedDate,
             _fileService.GetFileUrl(b.ImageNameInSystem, UploadDirectory.Blog),
                b.BlogAndCategories!.Select(bac => bac.BlogCategory)
                .Select(bc => new BlogCategoryViewModel(bc.Id, bc.Name!)).ToList(),
             b.Likes!.Select(l => new BlogLikeListViewModel(l.Id, l.Blog!.Title!, l.LikeCount)).ToList(),
             b.Comments!.Select(c => new BlogCommentListViewModel(c.Id, c.CommentDate, c.Text,
             $"{b.From.FirstName} {b.From.LastName}", c.Blog!.Title
             )).ToList())).ToListAsync();

            return View(blog);
        }
        #endregion

        #region Add

        [HttpGet("add", Name = "admin-blog-add")]
        public async Task<IActionResult> Add()
        {
            var model = new AddViewModel
            {
                Categories = await _dbContext.BlogCategories
                    .Select(c => new BlogCategoryViewModel(c.Id, c.Name!))
                    .ToListAsync(),
            };

            return View(model);
        }

        [HttpPost("add", Name = "admin-blog-add")]
        public async Task<IActionResult> Add(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return GetView(model);
            }

            foreach (var categoryId in model.CategoryIds)
            {
                if (!await _dbContext.BlogCategories.AnyAsync(c => c.Id == categoryId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Category with ({categoryId}) not found in database ");
                    return GetView(model);
                }
            }

            await AddProduct();

            await _dbContext.SaveChangesAsync();
            return RedirectToRoute("admin-blog-list");

            IActionResult GetView(AddViewModel model)
            {
                model.Categories = _dbContext.BlogCategories
                   .Select(c => new BlogCategoryViewModel(c.Id, c.Name))
                   .ToList();

                return View(model);
            }

            async Task AddProduct()
            {
                var imageNameInSystem = await _fileService.UploadAsync(model.Image!, UploadDirectory.Blog);

                var blog = new Blog
                {
                    Title = model.Title,
                    Description = model.Description,
                    PostedDate = DateTime.Now,
                    From = _userService.CurrentUser,
                    ImageName = model.Image!.FileName,
                    ImageNameInSystem = imageNameInSystem
                };

                await _dbContext.Blogs.AddAsync(blog);

                foreach (var catagoryId in model.CategoryIds)
                {
                    var blogAndCategory = new BlogAndCategory
                    {
                        BlogCategoryId = catagoryId,
                        Blog = blog
                    };

                    await _dbContext.BlogAndCategories.AddAsync(blogAndCategory);
                }
            }
        }
        #endregion

        #region Delete

        [HttpPost("delete/{id}", Name = "admin-blog-delete")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var products = await _dbContext.Blogs.FirstOrDefaultAsync(p => p.Id == id);
            if (products is null) return NotFound();

            _dbContext.Blogs.Remove(products);
            await _dbContext.SaveChangesAsync();
            return RedirectToRoute("admin-blog-list");
        }
        #endregion
    }
}





