﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Organic.Areas.Admin.ViewModels.Authentication;
using Organic.Areas.Admin.ViewModels.User;
using Organic.Areas.Admin.ViewModels.User.UserImage;
using Organic.Contracts.File;
using Organic.Database;
using Organic.Database.Models;
using Organic.Services.Abstracts;
using System.Drawing;
using System.Net;
using Organic.Areas.Admin.ViewModels.User;

namespace Organic.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/user")]
    [Authorize(Roles = "admin")]
    public class UserController : Controller
    {
        private readonly DataContext _dbContext;
        private readonly IFileService _fileService;

        public UserController(DataContext dbContext, IFileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
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
              u.Password!,
              u.Images!.Take(1).FirstOrDefault() != null
                   ? _fileService.GetFileUrl(u.Images!.Take(1).FirstOrDefault()!.ImageNameInFileSystem, UploadDirectory.User)
                   : String.Empty
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

            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password,
                RoleId = model.RoleId
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
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id)
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
                RoleId = user.RoleId,
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
                ModelState.AddModelError(string.Empty, "Role is not found");
                return GetView(model);
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.Password = model.Password;
            user.RoleId = model.RoleId;

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





