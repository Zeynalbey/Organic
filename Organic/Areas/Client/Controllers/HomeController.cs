using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Organic.Areas.Admin.ViewModels.Product.Count;
using Organic.Areas.Admin.ViewModels.Product.Discount;
using Organic.Areas.Admin.ViewModels.Product.Tag;
using Organic.Areas.Client.ViewModels.Home.Contact;
using Organic.Areas.Client.ViewModels.Product;
using Organic.Contracts.File;
using Organic.Database;
using Organic.Database.Models;
using Organic.Services.Abstracts;

namespace Organic.Areas.Client.Controllers
{
    [Area("client")]
    [Route("home")]
    public class HomeController : Controller
    {
        private readonly DataContext _dbContext;
        private readonly IFileService _fileService;
        public HomeController(DataContext dbContext, IFileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
        }

        [HttpGet("~/", Name = "client-home-index")]
        [HttpGet("index")]
        public async Task<IActionResult> IndexAsync([FromServices] IFileService fileService)
        {
            return View();
        }

        [HttpGet("contact", Name = "client-home-contact")]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost("contact", Name = "client-home-contact")]
        public async Task <ActionResult> Contact([FromForm] CreateViewModel contactViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            await _dbContext.Contacts.AddAsync(new Contact
            {
                Name = contactViewModel.Name,
                Email = contactViewModel.Email,
                Message = contactViewModel.Message,
                Phone = contactViewModel.PhoneNumber,
            });
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


     //   [HttpGet("quick/{id}", Name = "client-product-quick")]
     //   public async Task<IActionResult> Quick(int id)
     //   {

     //       var product = await _dbContext.Products.Include(p => p.ProductImages)
     //           .Include(p => p.ProductTags)
     //           .Include(p => p.ProductDiscountPercents)
     //           .Include(p => p.ProductCounts)
     //.FirstOrDefaultAsync(p => p.Id == id);

     //       if (product is null)
     //       {
     //           return NotFound();
     //       }

     //       var imageUrls = product.ProductImages!.Select(pi => new ProductImageViewModel
     //       {
     //           Id = pi.Id,
     //           ImageUrl = _fileService.GetFileUrl(pi.ImageNameInFileSystem, UploadDirectory.Product)
     //       });

     //       var viewModel = new ProductDetailViewModel(
     //           product.Id,
     //           product.Name!,
     //           product.Info!,
     //           product.Rating,
     //           product.RatingCount,
     //           product.Price,
     //           product.ProductDiscountPercents!.Select(pdp => new DiscountViewModel(pdp.Id, pdp.Percent)) ?? new List<DiscountViewModel>(),
     //           imageUrls,
     //           product.ProductTags!.Select(pt => pt.Tag).Select(t => new TagViewModel(t.Id, t.Name!)) ?? new List<TagViewModel>(),
     //           product.ProductCounts!.Select(pc => new CountViewModel(pc.Id, pc.Count)).ToList() ?? new List<CountViewModel>());

     //       return View(viewModel);
     //   }
    }


}
