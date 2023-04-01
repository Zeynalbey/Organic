using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Organic.Areas.Admin.ViewModels.User;
using Organic.Database;

namespace Organic.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/dashboard")]
    [Authorize(Roles = "admin,moderator")]
    public class DashboardController : Controller
    {
        private readonly DataContext _dbContext;

        public DashboardController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("AllLists", Name = "admin-dashboard-alllists")]
        public IActionResult AllList()
        {
            return View();
        }

        [HttpGet("Table", Name = "admin-dashboard-table")]
        public IActionResult Table()
        {
            return View();
        }

    }
}
