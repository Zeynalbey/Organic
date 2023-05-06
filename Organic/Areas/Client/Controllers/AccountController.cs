
using Organic.Areas.Client.ViewModels.OrderProducts;
using Organic.Database;
using Organic.Services.Abstracts;
using Organic.Services.Concretes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Organic.Areas.Client.Controllers
{
    [Area("client")]
    [Route("account")]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IUserService _userService;
        private readonly IOrderService _orderService;
        private readonly IFileService _fileService;

        public AccountController(
            DataContext dataContext,
            IUserService userService,
            IOrderService orderService,
            IFileService fileService)
        {
            _dataContext = dataContext;
            _userService = userService;
            _orderService = orderService;
            _fileService = fileService;
        }

        #region Order

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

            //string joinedString = "";
            //foreach (var str in model.Products)
            //{
            //    joinedString = string.Join(",", joinedString, $"{str.Name}: {str.Quantity} x {str.Price} ");
            //}

            //QrCodeGenerator qrCode = new QrCodeGenerator();

            //var image = qrCode.GenerateQrCode(joinedString);


            return View(model);
        }

        #endregion

    }
}
