using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Organic.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/dashboard")]
    [Authorize(Roles = "admin")]
    public class DashboardController : Controller
    {
        [HttpGet("Index",Name = "admin-dashboard-index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Table",Name = "admin-dashboard-table")]
        public IActionResult Table()
        {
            return View();
        }
    }
}
