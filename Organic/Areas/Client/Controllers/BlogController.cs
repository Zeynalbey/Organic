using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Organic.Areas.Admin.ViewModels.Blog;
using Organic.Areas.Client.ViewComponents;
using Organic.Areas.Client.ViewModels.Blog;
using Organic.Contracts.File;
using Organic.Contracts.ProductImage;
using Organic.Database;
using Organic.Database.Models;
using Organic.Services.Abstracts;
using System.Xml.Linq;

namespace Organic.Areas.Client.Controllers
{
    [Area("client")]
    [Route("client/blog")]
    public class BlogController : Controller
    {
        private readonly DataContext _dbContext;
        private readonly IFileService _fileService;
        private readonly IUserService _userService;
        public BlogController(DataContext dbContext, IFileService fileService, IUserService userService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
            _userService = userService;
        }

        #region Blogs

        [HttpGet("list", Name = "client-blog-list")]
        public async Task<IActionResult> ListAsync([FromServices] IFileService fileService, int page = 1)
        {
            var model = await _dbContext.Blogs
                .Skip((page - 1) * 6).Take(6)
                .Select(b => new BlogViewModel(b.Id,
                b.Title,
                b.Description!.Substring(0, 50),
                b.From!.FirstName,
                _fileService.GetFileUrl(b.From.ImageNameInSystem, UploadDirectory.User),
                b.PostedDate,
                b.ImageNameInSystem.FirstOrDefault() != null
                ? _fileService.GetFileUrl(b.ImageNameInSystem, UploadDirectory.Blog)
                : Image.DEFAULTIMAGE,
                b.BlogAndCategories!.Select(b => b.BlogCategory)
                .Select(b => new BlogCategoryViewModel(b!.Id, b.Name!)).ToList(),
                 b.LikeCount,
                 b.Comments!.Count))
                .ToListAsync();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = Math.Ceiling((decimal)model.Count() / 6);

            return View(model);
        }

        #endregion

        #region Single Blog

        [HttpGet("{id}", Name = "client-blog-single")]
        public async Task<IActionResult> Detail(int id, int page = 1)
        {



            var blog = await _dbContext.Blogs.Include(b => b.From).Include(b => b.Comments!).ThenInclude(c => c.From)
                .FirstOrDefaultAsync(b => b.Id == id);
            if (blog == null) return NotFound();




            blog.Comments!.Count();

            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == blog.From!.Id);
            if (user == null) return NotFound();
            var comment = await _dbContext.BlogComments.Include(bc => bc.From).FirstOrDefaultAsync(bc => bc.Id == id);

            var blogViewModel = new BlogItemViewModel
            {
                Id = blog.Id,
                Title = blog.Title!,
                Content = blog.Description!,
                Likes = blog.LikeCount,
                From = user.FirstName,
                PostedDate = blog.PostedDate,
                Image = blog.ImageNameInSystem.FirstOrDefault() != null
                ? _fileService.GetFileUrl(blog.ImageNameInSystem, UploadDirectory.Blog)
                : Image.DEFAULTIMAGE,
                Comments = blog.Comments!.Skip((page - 1) * 8).Take(8).Select(c => new BlogCommentItemViewModel
                {
                    Id = c.Id,
                    Content = c.Text!,
                    PostedDate = c.CommentDate.ToString(),
                    From = c.From!.FirstName,
                    Image = _fileService.GetFileUrl(c.From.ImageNameInSystem, UploadDirectory.User)
                }).ToList()
            };

            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = Math.Ceiling((decimal)blog.Comments.Count() / 8);

            return View(blogViewModel);
        }

        #endregion

        #region CommentAdd

        [HttpPost("{id}", Name = "client-blog-single")]
        public async Task<IActionResult> Detail(BlogItemViewModel model, int id)
        {
            var blog = await _dbContext.Blogs.FirstOrDefaultAsync(b => b.Id == id);
            if (blog is null) return NotFound();

            var comment = new BlogComment
            {
                Text = model.CommentText,
                BlogId = model.Id,
                From = _userService.CurrentUser,
                CommentDate = DateTime.Now
            };

            await _dbContext.AddAsync(comment);
            await _dbContext.SaveChangesAsync();
            return RedirectToRoute("client-blog-single", new { id = blog!.Id });
        }

        #endregion

        #region Like

        [HttpGet("Like/{Likeid}", Name = "blog-like")]
        public async Task<IActionResult> LikeAsync(int Likeid)
        {
            var blog = await _dbContext.Blogs.FindAsync(Likeid);
            if (blog is null) return NotFound();

            blog.LikeCount++;
            await _dbContext.SaveChangesAsync();

            return RedirectToRoute("client-blog-single", new { id = blog!.Id });
        }

        #endregion

    }
}
