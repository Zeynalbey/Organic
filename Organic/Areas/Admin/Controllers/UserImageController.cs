
using Organic.Contracts.File;
using Organic.Database;
using Organic.Database.Models;
using Organic.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Organic.Areas.Admin.ViewModels.User.UserImage;

namespace Organic.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/user")]
    [Authorize(Roles = "admin")]
    public class UserImageController : Controller
    {
        private readonly DataContext _dbContext;
        private readonly IFileService _fileService;

        public UserImageController(
            DataContext dbContext,
            IFileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
        }
        #region List

        [HttpGet("{userId}/image/list", Name = "admin-user-image-list")]
        public async Task<IActionResult> ListAsync([FromRoute] Guid userId)
        {
            var user = await _dbContext.Users.Include(b => b.Images)
                .FirstOrDefaultAsync(b => b.Id == userId);


            if (user is null)
            {
                return NotFound();
            }

            var model = new UserImageViewModel { UserId = user.Id };

            model.Images = user.Images.Select(bi => new UserImageViewModel.ListItem
            {
                Id = bi.Id,
                ImageUrL = _fileService.GetFileUrl(bi.ImageNameInFileSystem, UploadDirectory.User),
            }).ToList();

            return View(model);
        }

        #endregion

        #region Add

        [HttpGet("{userId}/image/add", Name = "admin-user-image-add")]
        public async Task<IActionResult> AddAsync()
        {
            return View(new AddViewModel());
        }

        [HttpPost("{userId}/image/add", Name = "admin-user-image-add")]
        public async Task<IActionResult> AddAsync([FromRoute] Guid userId, [FromForm] AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _dbContext.Users.FirstOrDefaultAsync(b => b.Id == userId);
            if (user is null)
            {
                return NotFound();
            }

            var imageNameInSystem = await _fileService.UploadAsync(model.Image!, UploadDirectory.User);

            var userImage = new UserImage
            {
                User = user,
                ImageName = model.Image!.FileName,
                ImageNameInFileSystem = imageNameInSystem
            };

            await _dbContext.AddAsync(userImage);

            await _dbContext.SaveChangesAsync();

            return RedirectToRoute("admin-user-image-list", new { userId = userId });
        }

        #endregion

        #region Delete

        [HttpPost("{userId}/image/{userImageId}/delete", Name = "admin-user-image-delete")]
        public async Task<IActionResult> DeleteAsync(Guid userId, Guid userImageId)
        {
            var userImage = await _dbContext.UserImages
                .FirstOrDefaultAsync(bi => bi.Id == userImageId && bi.UserId == userId);
            if (userImage is null)
            {
                return NotFound();
            }

            await _fileService.DeleteAsync(userImage.ImageNameInFileSystem, UploadDirectory.User);

            _dbContext.UserImages.Remove(userImage);
            await _dbContext.SaveChangesAsync();

            return RedirectToRoute("admin-user-image-list", new { userId = userId });
        }

        #endregion

    }
}
