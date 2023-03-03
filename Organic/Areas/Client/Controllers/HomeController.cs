using Microsoft.AspNetCore.Mvc;
using Organic.Areas.Client.ViewModels.Home.Contact;
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
    }


}
