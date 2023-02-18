using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Organic.Areas.Admin.ViewModels.Product.Tag;
using Organic.Database;
using Organic.Database.Models;
using Organic.Services.Abstracts;


namespace Organic.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/tag")]
    [Authorize(Roles = "admin, moderator")]
    public class TagController : Controller
    {
        private readonly DataContext _dbContext;
        private readonly IFileService _fileService;

        public TagController(DataContext dbContext, IFileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
        }

        #region List

        [HttpGet("list", Name = "admin-tag-list")]
        public async Task <ActionResult> List()
        {
            var model = await _dbContext.Tags
                .Select(c => new TagViewModel(c.Id, c.Name!))
                .ToListAsync();

            return View(model);
        }

        #endregion

        #region Add

        [HttpGet("add", Name = "admin-tag-add")]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost("add", Name = "admin-tag-add")]
        public async Task <ActionResult> Add(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _dbContext.Tags.AddAsync(new Tag
            {
                Name = model.Name
            });

            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }

        #endregion

        #region Update

        [HttpGet("update/{id}", Name = "admin-tag-update")]
        public async Task <ActionResult> UpdateAsync([FromRoute] int id)
        {
            var tag = await _dbContext.Tags.FirstOrDefaultAsync(b => b.Id == id);
            if (tag is null)
            {
                return NotFound();
            }

            return View(new UpdateViewModel { Id = tag.Id, Name = tag.Name!});
        }

        [HttpPost("update/{id}", Name = "admin-tag-update")]
        public async Task <ActionResult> UpdateAsync([FromRoute] int id, [FromForm] UpdateViewModel model)
        {
            var tag = await _dbContext.Tags.FirstOrDefaultAsync(b => b.Id == id);
            if (tag is null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            tag.Name = model.Name;

            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }   

        #endregion

        #region Delete

        [HttpPost("delete/{id}", Name = "admin-tag-delete")]
        public async Task <ActionResult> DeleteAsync([FromRoute] int id)
        {
            var tag = await _dbContext.Tags.FirstOrDefaultAsync(b => b.Id == id);
            if (tag is null)
            {
                return NotFound();
            }

            _dbContext.Tags.Remove(tag);
            await _dbContext.SaveChangesAsync();

            return RedirectToRoute("admin-tag-list");
        }

        #endregion
    }
}
