﻿using Microsoft.AspNetCore.Mvc;
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
using Organic.Database.Models;
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
                b.ImageNameInSystem.FirstOrDefault() != null
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
            var blog = await _dbContext.Blogs.Include(b => b.Comments)
                .Include(b=>b.From)
                .Include(b => b.Likes).FirstOrDefaultAsync(b => b.Id == id);

            if (blog == null) return NotFound();
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == blog.From.Id);
            if (user == null) return NotFound();

            var blogViewModel = new BlogItemViewModel
            {
                Id = blog.Id,
                Title = blog.Title!,
                Content = blog.Description!,
                From = user.FirstName,
                PostedDate = blog.PostedDate,
                Image = blog.ImageNameInSystem.FirstOrDefault() != null
                ? _fileService.GetFileUrl(blog.ImageNameInSystem, UploadDirectory.Blog)
                : Image.DEFAULTIMAGE,
                Comments = blog.Comments!.Select(c => new BlogCommentItemViewModel
                {
                    Id = c.Id,
                    Content = c.Text!
                }).ToList(),

                Likes = blog.Likes!.Select(l => new BlogLikeItemViewModel
                {
                    Id = l.Id
                }).ToList()
            };

            return View(blogViewModel);
        }
    }
}
