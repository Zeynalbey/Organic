using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Organic.Areas.Admin.ViewModels.Product;
using Organic.Areas.Admin.ViewModels.Product.Category;
using Organic.Database;
using Organic.Database.Models;
using Organic.Services.Abstracts;

namespace Organic.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/category")]
    [Authorize(Roles = "admin, moderator")]
    public class CategoryController : Controller
    {
        private readonly DataContext _dbContext;
        private readonly IFileService _fileService;

        public CategoryController(DataContext dbContext, IFileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
        }

        #region List

        [HttpGet("list", Name = "admin-category-list")]
        public async Task <ActionResult> List()
        {
            var model = await _dbContext.Categories
                .Select(c => new CategoryViewModel(c.Id, c.Name!, c.IconClass!))
                .ToListAsync();

            return View(model);
        }

        #endregion

        #region Add

        [HttpGet("add", Name = "admin-category-add")]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost("add", Name = "admin-category-add")]
        public async Task <ActionResult> Add(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _dbContext.Categories.AddAsync(new Category
            {
                Name = model.Name,
                IconClass = model.IconClass
            });

            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }

        #endregion

        #region Update

        [HttpGet("update/{id}", Name = "admin-category-update")]
        public async Task <ActionResult> UpdateAsync([FromRoute] int id)
        {
            var category = await _dbContext.Categories.FirstOrDefaultAsync(b => b.Id == id);
            if (category is null)
            {
                return NotFound();
            }

            return View(new UpdateViewModel { Id = category.Id, Name = category.Name!, IconClass = category.IconClass});
        }

        [HttpPost("update/{id}", Name = "admin-category-update")]
        public async Task <ActionResult> UpdateAsync([FromRoute] int id, [FromForm] UpdateViewModel model)
        {
            var category = await _dbContext.Categories.FirstOrDefaultAsync(b => b.Id == id);
            if (category is null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            category.Name = model.Name;
            category.IconClass = model.IconClass;   

            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }   

        #endregion

        #region Delete

        [HttpPost("delete/{id}", Name = "admin-category-delete")]
        public async Task <ActionResult> DeleteAsync([FromRoute] int id)
        {
            var category = await _dbContext.Categories.FirstOrDefaultAsync(b => b.Id == id);
            if (category is null)
            {
                return NotFound();
            }

            _dbContext.Categories.Remove(category);
            await _dbContext.SaveChangesAsync();

            return RedirectToRoute("admin-category-list");
        }

        #endregion
    }
}
