using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Organic.Areas.Client.Controllers
{
    [Area("client")]
    [Route("Error")]
    public class ErrorController : Controller
    {
        [HttpGet("Index")]
        public IActionResult MyError() 
        {
            return View();  
        }
    }
}
