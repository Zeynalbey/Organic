using Organic.Areas.Client.ViewModels.Authentication;
using Organic.Areas.Client.ViewModels.OrderProducts;
using Organic.Database;
using Organic.Database.Models;
using Organic.Services.Abstracts;
using Organic.Services.Concretes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_Final.Areas.Client.Controllers
{
    [Area("client")]
    [Route("account")]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IUserService _userService;
        private readonly IOrderService _orderService;

        public AccountController(
            DataContext dataContext,
            IUserService userService,
            IOrderService orderService)
        {
            _dataContext = dataContext;
            _userService = userService;
            _orderService = orderService;
        }

        //[HttpGet("dashboard", Name = "client-account-dashboard")]
        //public IActionResult Dashboard()
        //{
        //    return View();
        //}


        [HttpGet("orders", Name = "client-account-orders")]
        public async Task<IActionResult> Order()
        {

            var model = new OrdersProductsViewModel
            {
                Products = await _dataContext.OrderProducts.Include(op=> op.Order).Where(o=>o.Order!.UserId == _userService.CurrentUser.Id)
                  .Select(p => new OrdersProductsViewModel.ItemViewModel
                  {
                      Name = p.Product!.Name,
                      Price = p.Product.Price,
                      Quantity = p.Quantity,
                      Total = p.Product.Price * p.Quantity,
                  }).ToListAsync(),

                Summary = new OrdersProductsViewModel.SummaryViewModel
                {
                    Total = await _dataContext.OrderProducts
                    .SumAsync(bp => bp.Product!.Price * bp.Quantity)
                }
            };

            return View(model);
        }

    }
}
