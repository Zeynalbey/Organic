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

        [HttpGet("Index", Name = "admin-dashboard-index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Table", Name = "admin-dashboard-table")]
        public IActionResult Table()
        {
            return View();
        }

        [HttpGet("AllLists", Name = "admin-dashboard-alllists")]
        public IActionResult AllList()
        {
            return View();
        }
    }
}
