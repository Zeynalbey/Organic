using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Organic.Areas.Admin.ViewModels.Product.Count;
using Organic.Areas.Admin.ViewModels.Product.Discount;
using Organic.Areas.Admin.ViewModels.Product.Tag;
using Organic.Areas.Client.ViewModels.Product;
using Organic.Contracts.File;
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

        [HttpGet("Rate/{id}/{rate}")]
        public ActionResult Rate(int id, int rate)
        {
            var product = _dbContext.Products.Find(id);
            if (product != null)
            {
                product.Rating += rate;
                product.RatingCount++;
                _dbContext.SaveChanges();
            }
            return RedirectToRoute("client-home-index");
        }       
    }
}
