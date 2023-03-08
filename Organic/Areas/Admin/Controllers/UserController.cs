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
using Organic.Areas.Admin.ViewModels.User;
using Organic.Contracts.Identity;
using Organic.Areas.Client.ViewComponents;

namespace Organic.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/user")]
    [Authorize(Roles = "admin")]
    public class UserController : Controller
    {
        private readonly DataContext _dbContext;
        private readonly IFileService _fileService;
        private readonly IUserService _userService;

        public UserController(DataContext dbContext, IFileService fileService, IUserService userService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
            _userService = userService;
        }

        #region List
        [HttpGet("list", Name = "admin-user-list")]
        public async Task<IActionResult> ListAsync()
        {
            var user = await _dbContext.Users.Select(u => new UserListViewModel(
              u.Id,
              u.FirstName,
              u.LastName,
              u.Email,
              u.Role!.Name!,
              u.Password!
              )).ToListAsync();

            return View(user);
        }
        #endregion

        #region AddUser

        [HttpGet("add", Name = "admin-user-add")]
        public IActionResult Add()
        {
            var model = new ViewModels.User.AddViewModel
            {
                Roles = _dbContext.Roles.Select(r => new RoleListViewModel(r.Id, r.Name)).ToList()
            };

            return View(model);
        }

        [HttpPost("add", Name = "admin-user-add")]
        public async Task<IActionResult> Add(ViewModels.User.AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return GetView(model);
            }

            if (!_dbContext.Roles.Any(c => c.Id == model.RoleId))
            {
                ModelState.AddModelError(string.Empty, "Something went wrong");
                //_logger.LogWarning($"Category with id({categoryId}) not found in db ");
                return GetView(model);
            }

            var imageNameInSystem = await _fileService.UploadAsync(model.Image!, UploadDirectory.User);
            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password,
                RoleId = model.RoleId,
                ImageName = model.Image!.Name,
                ImageNameInSystem = imageNameInSystem
            };

            IActionResult GetView(ViewModels.User.AddViewModel model)
            {
                model.Roles = _dbContext.Roles
                   .Select(c => new RoleListViewModel(c.Id, c.Name))
                   .ToList();
                return View(model);
            }
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            return RedirectToRoute("admin-user-list");
        }

        #endregion

        #region UpdateUser

        [HttpGet("update/{id}", Name = "admin-user-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, int roleId)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(b => b.Id == id);
            
            if (user is null)
            {
                return NotFound();
            }

            var model = new UpdateViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email= user.Email,
                Password= user.Password,
                RoleId = user.RoleId,
                İmageUrl = _fileService.GetFileUrl(user.ImageNameInSystem, UploadDirectory.Slider),
                Roles = _dbContext.Roles
                    .Select(r => new RoleListViewModel(r.Id, r.Name))
                    .ToList()
            };

            return View(model);
        }

        [HttpPost("update/{id}", Name = "admin-user-update")]
        public async Task<IActionResult> UpdateAsync(UpdateViewModel model)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(b => b.Id == model.Id);
            
            if (user is null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return GetView(model);
            }

            if (!_dbContext.Roles.Any(a => a.Id == model.RoleId))
            {
                ModelState.AddModelError(string.Empty, "Role is not found!");
                return GetView(model);
            }

            var imageNameInSystem = await _fileService.UploadAsync(model.Image!, UploadDirectory.User);

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.Password = model.Password;
            user.RoleId = model.RoleId;
            user.ImageName = model.Image!.FileName;
            user.ImageNameInSystem = imageNameInSystem;

            await _dbContext.SaveChangesAsync();

            return RedirectToRoute("admin-user-list");

            IActionResult GetView(UpdateViewModel model)
            {
                model.Roles = _dbContext.Roles
                    .Select(r => new RoleListViewModel(r.Id, r.Name))
                    .ToList();

                return View(model);
            }
        }

        #endregion

        #region DeleteUser

        [HttpPost("delete/{id}", Name = "admin-user-delete")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(b => b.Id == id);
            if (user is null)
            {
                return NotFound();
            }

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();

            return RedirectToRoute("admin-user-list");
        }




















        #endregion

    }
}





