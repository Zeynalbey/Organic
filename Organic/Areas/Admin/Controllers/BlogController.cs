using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Organic.Areas.Admin.ViewModels.Authentication;
using Organic.Areas.Admin.ViewModels.User;
using Organic.Contracts.File;
using Organic.Database;
using Organic.Database.Models;
using Organic.Services.Abstracts;
using System.Drawing;
using System.Net;
using Organic.Contracts.Identity;
using Organic.Areas.Admin.ViewModels.Blog;

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

        public BlogController(DataContext dbContext, IFileService fileService, IUserService userService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
            _userService = userService;
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
             b.Likes!.Select(l => new BlogLikeListViewModel(l.Id, l.Blog!.Title!, l.LikeCount)).ToList(),
             b.Comments!.Select(c => new BlogCommentListViewModel(c.Id, c.CommentDate, c.Text, 
             $"{b.From.FirstName} {b.From.LastName}", c.Blog!.Title
             )).ToList())).ToListAsync();

            return View(blog);
        }
        #endregion


    }
}





