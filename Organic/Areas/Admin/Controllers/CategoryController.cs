using Microsoft.AspNetCore.Mvc;

namespace Organic.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
