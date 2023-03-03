using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Organic.Database;
using Organic.Services.Abstracts;

namespace Organic.Areas.Client.Controllers
{
    [Area("client")]
    [Route("client/product")]
    public class AllProductController : Controller
    {
        private readonly DataContext _dbContext;
        private readonly IFileService _fileService;
        public AllProductController(DataContext dbContext, IFileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
        }

        [HttpGet("list", Name = "client-product-list")]
        public async Task<IActionResult> ListAsync([FromServices] IFileService fileService)
        {
            return View();
        }

        [HttpGet("Rate/{id}/{rate}")]
        public ActionResult Rate(int id, int rate)
        {
            var product = _dbContext.Products.Find(id);
            if (product != null)
            {
                product.Rating += rate;
                product.RatingCount ++;
                _dbContext.SaveChanges();
            }
            return RedirectToRoute("client-home-index");
        }

    }

}
