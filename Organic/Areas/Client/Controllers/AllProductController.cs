using Microsoft.AspNetCore.Mvc;
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
    }
}
