using Organic.Areas.Admin.ViewModels;
using Organic.Contracts.File;
using Organic.Database;
using Organic.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Organic.Areas.Admin.ViewModels.User;

namespace Organic.Areas.Admin.ViewComponents
{
    [Area("Admin")]
    [ViewComponent(Name = "UserTable")]
    public class UserTable : ViewComponent
    {
        private readonly DataContext _dbContext;
        private readonly IFileService _fileService;

        public UserTable(DataContext dbContext, IFileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _dbContext.Users.OrderByDescending(u => u.CreatedAt).Take(4).Select(u => new UserListViewModel(
                u.Id,
                u.FirstName,
                u.LastName,
                u.Email,
                u.Role!.Name!,
                u.Password!
                )).ToListAsync();

            return View(user);
        }
    }
}
