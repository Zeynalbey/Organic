using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Organic.Areas.Admin.ViewModels.Authentication;
using Organic.Areas.Admin.ViewModels.User;
using Organic.Database;
using Organic.Database.Models;
using System.Drawing;

namespace Organic.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/user")]
    [Authorize(Roles = "admin")]
    public class UserController : Controller
    {
        private readonly DataContext _dbContext;

        public UserController(DataContext dbContext)
        {
            _dbContext = dbContext;
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
            var model = new AddViewModel
            {
                Roles = _dbContext.Roles.Select(r => new RoleListViewModel(r.Id, r.Name)).ToList()
            };

            return View(model);
        }

        [HttpPost("add", Name = "admin-user-add")]
        public async Task<IActionResult> Add(AddViewModel model)
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

            IActionResult GetView(AddViewModel model)
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






//#region Update

//[HttpGet("update/{id}", Name = "admin-book-update")]
//public async Task<IActionResult> UpdateAsync([FromRoute] int id)
//{
//    var book = await _dataContext.Books.Include(b => b.BookCategories).FirstOrDefaultAsync(b => b.Id == id);
//    if (book is null)
//    {
//        return NotFound();
//    }

//    var model = new AddViewModel
//    {
//        Id = book.Id,
//        Title = book.Title,
//        Price = book.Price,
//        AuthorId = book.AuthorId,
//        Authors = _dataContext.Authors
//            .Select(a => new AuthorListItemViewModel(a.Id, $"{a.FirstName} {a.LastName}"))
//            .ToList(),
//        Categories = _dataContext.Categories
//            .Select(c => new CategoryListItemViewModel(c.Id, c.Title))
//            .ToList(),
//        CategoryIds = book.BookCategories.Select(bc => bc.CategoryId).ToList(),
//    };

//    return View(model);
//}

//[HttpPost("update/{id}", Name = "admin-book-update")]
//public async Task<IActionResult> UpdateAsync(AddViewModel model)
//{
//    var book = await _dataContext.Books.Include(b => b.BookCategories).FirstOrDefaultAsync(b => b.Id == model.Id);
//    if (book is null)
//    {
//        return NotFound();
//    }

//    if (!ModelState.IsValid)
//    {
//        return GetView(model);
//    }

//    if (!_dataContext.Authors.Any(a => a.Id == model.AuthorId))
//    {
//        ModelState.AddModelError(string.Empty, "Author is not found");
//        return GetView(model);
//    }

//    foreach (var categoryId in model.CategoryIds)
//    {
//        if (!_dataContext.Categories.Any(c => c.Id == categoryId))
//        {
//            ModelState.AddModelError(string.Empty, "Something went wrong");
//            _logger.LogWarning($"Category with id({categoryId}) not found in db ");
//            return GetView(model);
//        }

//    }


//    //await _fileService.DeleteAsync(book.ImageNameInFileSystem, UploadDirectory.Book);
//    //var imageFileNameInSystem = await _fileService.UploadAsync(model.Image, UploadDirectory.Book);

//    await UpdateBookAsync();

//    return RedirectToRoute("admin-book-list");




//    IActionResult GetView(AddViewModel model)
//    {
//        model.Authors = _dataContext.Authors
//            .Select(a => new AuthorListItemViewModel(a.Id, $"{a.FirstName} {a.LastName}"))
//            .ToList();

//        model.Categories = _dataContext.Categories
//           .Select(c => new CategoryListItemViewModel(c.Id, c.Title))
//           .ToList();

//        model.CategoryIds = book.BookCategories.Select(bc => bc.CategoryId).ToList();

//        return View(model);
//    }

//    async Task UpdateBookAsync()
//    {
//        book.Title = model.Title;
//        book.AuthorId = model.AuthorId;
//        book.Price = model.Price;

//        var categoriesInDb = book.BookCategories.Select(bc => bc.CategoryId).ToList();
//        var categoriesToRemove = categoriesInDb.Except(model.CategoryIds).ToList();
//        var categoriesToAdd = model.CategoryIds.Except(categoriesInDb).ToList();

//        book.BookCategories.RemoveAll(bc => categoriesToRemove.Contains(bc.CategoryId));

//        foreach (var categoryId in categoriesToAdd)
//        {
//            var bookCategory = new BookCategory
//            {
//                CategoryId = categoryId,
//                Book = book,
//            };

//            _dataContext.BookCategories.Add(bookCategory);
//        }

//        _dataContext.SaveChanges();
//    }
//}

//#endregion

//#region Delete

//[HttpPost("delete/{id}", Name = "admin-book-delete")]
//public async Task<IActionResult> DeleteAsync([FromRoute] int id)
//{
//    var book = await _dataContext.Books.FirstOrDefaultAsync(b => b.Id == id);
//    if (book is null)
//    {
//        return NotFound();
//    }

//    //await _fileService.DeleteAsync(book.ImageNameInFileSystem, UploadDirectory.Book);

//    _dataContext.Books.Remove(book);
//    await _dataContext.SaveChangesAsync();

//    return RedirectToRoute("admin-book-list");
//}

//#endregion

