﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Organic.Areas.Admin.ViewModels.Product;
using Organic.Areas.Admin.ViewModels.Slider;
using Organic.Contracts.File;
using Organic.Database;
using Organic.Database.Models;
using Organic.Services.Abstracts;


namespace Organic.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/slider")]
    [Authorize(Roles = "admin, moderator")]
    public class SliderController : Controller
    {
        private readonly DataContext _dataContext;

        private readonly IFileService _fileService;

        public SliderController(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        #region List
        [HttpGet("list", Name = "admin-slider-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.Sliders.Select(s => new ListViewModel(
                s.Id,
                s.Product!.Name!,
                s.Title!,
                s.Description!,
                _fileService.GetFileUrl(s.ImageNameInSystem, UploadDirectory.Slider),
                s.CreatedAt)).ToListAsync();

            return View(model);
        }
        #endregion

        #region Add

        [HttpGet("add", Name = "admin-slider-add")]
        public async Task<IActionResult> AddAsync()
        {
            var model = new AddViewModel
            {
                Products = _dataContext.Products
                    .Select(c => new ProductViewModel(c.Id, c.Name))
                    .ToList(),
            };
            return View(model);
        }

        [HttpPost("add", Name = "admin-slider-add")]
        public async Task<IActionResult> Add(AddViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (!_dataContext.Products.Any(a => a.Id == model.ProductId))
            {
                ModelState.AddModelError(string.Empty, "Product is not found");
                return await GetView(model);
            }

            var imageNameInSystem = await _fileService.UploadAsync(model.Image!, UploadDirectory.Slider);

            var slider = new Slider
            {
                ProductId = model.ProductId,
                Title = model.Title,
                Description = model.Description,
                ImageName = model.Image!.FileName,
                ImageNameInSystem = imageNameInSystem,
               // CreatedAt = DateTime.Now,
            };


            async Task<IActionResult> GetView(AddViewModel model)
            {
                model.Products = await _dataContext.Products
                    .Select(a => new ProductViewModel(a.Id, a.Name))
                    .ToListAsync();

                return View(model);
            }

            _dataContext.Sliders.Add(slider);

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-slider-list");
        }
        #endregion

        #region Delete

        [HttpPost("delete/{id}", Name = "admin-slider-delete")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var slider = await _dataContext.Sliders.FirstOrDefaultAsync(b => b.Id == id);
            if (slider is null)
            {
                return NotFound();
            }
            await _fileService.DeleteAsync(slider.ImageNameInSystem, UploadDirectory.Slider);
            _dataContext.Sliders.Remove(slider);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-slider-list");
        }
        #endregion
    }

}
