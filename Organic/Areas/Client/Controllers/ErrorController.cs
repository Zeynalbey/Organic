using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Organic.Areas.Client.Controllers
{
    [Area("client")]
    [Route("Error")]
    public class ErrorController : Controller
    {

        public readonly ILogger<BlogController> _logger;

        public ErrorController(ILogger<BlogController> logger)
        {
            _logger = logger;
        }

        //[Route("Error/{StatusCode}")]
        //public IActionResult HttpStatusCodeHandler(int StatusCode) //bu statuscode gozleyir.
        //{
        //    var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
        //    switch (StatusCode)
        //    {
        //        case 404:
        //            ViewBag.ErrorMessage = "Sorry, the resource you requested could not be found";
        //            ViewBag.RouteOfException = statusCodeResult.OriginalPath;
        //            _logger.LogWarning($"404 Error Occured. Path = {statusCodeResult.OriginalPath} and QueryString = {statusCodeResult.OriginalQueryString}");
        //            break;
        //    }
        //    return View("NotFound");  //burda robot view verecem.
        //}

        public IActionResult Index() //bu statuscode gozleyir.
        {
            return View("/Views/Shared/NotFound.cshtml");  //burda robot view verecem.
        }

        [HttpGet("Index")]
        public IActionResult MyError() 
        {
            return View();  
        }
    }
}
