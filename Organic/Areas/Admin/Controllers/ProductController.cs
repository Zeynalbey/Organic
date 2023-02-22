using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Organic.Areas.Admin.ViewModels.Product;
using Organic.Areas.Admin.ViewModels.Product.Count;
using Organic.Areas.Admin.ViewModels.Product.Rate;
using Organic.Areas.Admin.ViewModels.User;
using Organic.Contracts.File;
using Organic.Database;
using Organic.Services.Abstracts;

namespace Organic.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/product")]
    [Authorize(Roles = "admin, moderator")]
    public class ProductController : Controller
    {
        private readonly DataContext _dbContext;
        private readonly IFileService _fileService;
        public ProductController(DataContext dbContext, IFileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
        }

        #region List

        [HttpGet("list", Name = "admin-product-list")]
        public async Task<IActionResult> ListAsync()
        {
            var product = await _dbContext.Products
                .Select(p=> new ListItemViewModel(p.Id, p.Name, p.Info, p.Price, p.CreatedAt,p.Category!.Id,p.Category.Name!, p.ProductRates!
                .Select(r=> new RateViewModel(r.Id, r.Rating)).ToList(),
                 p.ProductCounts!
                .Select(pc=> new CountViewModel(pc.Id, pc.Count)).ToList())).ToListAsync();

            return View(product);
        }
        #endregion
    }
}
