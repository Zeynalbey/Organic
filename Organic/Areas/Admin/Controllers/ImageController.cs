using Organic.Contracts.File;
using Organic.Database;
using Organic.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Organic.Areas.Admin.ViewModels.Product.Image;
using Organic.Database.Models;

namespace Organic.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/user")]
    [Authorize(Roles = "admin, moderator")]
    public class ImageController : Controller
    {
        private readonly DataContext _dbContext;
        private readonly IFileService _fileService;

        public ImageController(
            DataContext dbContext,
            IFileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
        }
        #region List

        [HttpGet("{productId}/image/list", Name = "admin-product-image-list")]
        public async Task<IActionResult> ListAsync([FromRoute] int productId)
        {
            var product = await _dbContext.Products.Include(b => b.ProductImages)
                .FirstOrDefaultAsync(b => b.Id == productId);


            if (product is null)
            {
                return NotFound();
            }

            var model = new ImageViewModel { ProductId = product.Id };

            model.Images = product.ProductImages!.Select(bi => new ImageViewModel.ListItem
            {
                Id = bi.Id,
                ImageUrL = _fileService.GetFileUrl(bi.ImageNameInFileSystem, UploadDirectory.Product),
            }).ToList();

            return View(model);
        }

        #endregion

        #region Add

        [HttpGet("{productId}/image/add", Name = "admin-product-image-add")]
        public async Task<IActionResult> AddAsync()
        {
            return View(new AddViewModel());
        }

        [HttpPost("{productId}/image/add", Name = "admin-product-image-add")]
        public async Task<IActionResult> AddAsync([FromRoute] int productId, [FromForm] AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var product = await _dbContext.Products.FirstOrDefaultAsync(b => b.Id == productId);
            if (product is null)
            {
                return NotFound();
            }

            var imageNameInSystem = await _fileService.UploadAsync(model.Image!, UploadDirectory.Product);

            var productImage = new ProductImage
            {
                Product = product,
                ImageName = model.Image!.FileName,
                ImageNameInFileSystem = imageNameInSystem
            };

            await _dbContext.AddAsync(productImage);

            await _dbContext.SaveChangesAsync();

            return RedirectToRoute("admin-product-image-list", new { productId = productId });
        }

        #endregion

        //#region Delete

        //[HttpPost("{userId}/image/{userImageId}/delete", Name = "admin-user-image-delete")]
        //public async Task<IActionResult> DeleteAsync(Guid userId, Guid userImageId)
        //{
        //    var userImage = await _dbContext.UserImages
        //        .FirstOrDefaultAsync(bi => bi.Id == userImageId && bi.UserId == userId);
        //    if (userImage is null)
        //    {
        //        return NotFound();
        //    }

        //    await _fileService.DeleteAsync(userImage.ImageNameInFileSystem, UploadDirectory.User);

        //    _dbContext.UserImages.Remove(userImage);
        //    await _dbContext.SaveChangesAsync();

        //    return RedirectToRoute("admin-user-image-list", new { userId = userId });
        //}

        //#endregion

    }
}
