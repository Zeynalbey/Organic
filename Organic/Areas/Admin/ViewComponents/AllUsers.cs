using Organic.Areas.Admin.ViewModels;
using Organic.Contracts.File;
using Organic.Database;
using Organic.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Organic.Areas.Admin.ViewModels.Authentication;

namespace Organic.Areas.Admin.ViewComponents
{
    [Area("Admin")]
    [ViewComponent(Name = "AllUsers")]
    public class AllUsers : ViewComponent
    {
        private readonly DataContext _dbContext;
        private readonly IFileService _fileService;

        public AllUsers(DataContext dbContext, IFileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _dbContext.Users.Select(u => new UserListViewModel(
                u.FirstName,
                u.LastName,
                u.Email,
                u.Role!.Name!
                )).ToListAsync();

            return View(user);
        }
    }
}
