﻿
using Organic.Areas.Client.ViewModels.OrderProducts;
using Organic.Database;
using Organic.Database.Models;
using Organic.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Organic.Contracts.File;
using Organic.Services.Concretes;
using Organic.Contracts.ProductImage;
using Organic.Areas.Client.ViewComponents;
using Organic.Areas.Admin.ViewModels.Product.Discount;

namespace Organic.Areas.Client.Controllers
{
    [Area("client")]
    [Route("order")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IUserService _userService;
        private readonly IOrderService _orderService;
        private readonly IFileService _fileService;
        public OrderController(
            DataContext dataContext,
            IFileService fileService,
            IUserService userService,
            IOrderService orderService)
        {
            _dataContext = dataContext;
            _userService = userService;
            _orderService = orderService;
            _fileService = fileService;
        }

        #region CheckOut'

        [HttpGet("checkout", Name = "client-order-checkout")]
        public async Task<IActionResult> CheckOut()
        {
            //var discountPrice = _dataContext.ProductDiscountPercents!.FirstOrDefault(dp => dp.Product.Id == product.Id);

            var model = new OrdersProductsViewModel
            {

                Products = await _dataContext.BasketProducts
                .Where(p => p.Basket!.UserId == _userService.CurrentUser.Id)
                  .Select(p => new OrdersProductsViewModel.ItemViewModel
                  {
                      Name = p.Product!.Name,
                      Image = p.Product.ProductImages!.Take(1).FirstOrDefault() != null
                   ? _fileService.GetFileUrl(p.Product.ProductImages!.Take(1).FirstOrDefault()!.ImageNameInFileSystem, UploadDirectory.Product)
                   : Image.DEFAULTIMAGE,
                      Price = p.Product.Price,
                      Quantity = p.Quantity,
                      Total = p.Product.ProductDiscountPercents!.FirstOrDefault() != null
                      ? p.Product.Price * p.Quantity
                      : p.Product.ProductDiscountPercents!.FirstOrDefault(p => p.Percent != null)!.Percent

                      * p.Quantity,
                  }).ToListAsync(),


                Summary = new OrdersProductsViewModel.SummaryViewModel
                {
                    Total = await _dataContext.BasketProducts
                   .Where(pu => pu.Basket!.UserId == _userService.CurrentUser.Id)
                    .SumAsync(bp => bp.Product!.Price * bp.Quantity)
                }

            };



            return View(model);
        }
        #endregion

        #region PlaceOrder'

        [HttpPost("placeorder", Name = "client-order-placeorder")]
        public async Task<IActionResult> PlaceOrder()
        {
            var basketProducts = await _dataContext.BasketProducts
                .Include(p => p.Product)
                .Where(p => p.Basket!.UserId == _userService.CurrentUser.Id)
                .ToListAsync();

            var order = await CreateOrder();

            await CreateFullOrderProductAync(order, basketProducts);
            order.Total = order.OrderProducts!.Sum(p => p.Total);

            await ResetBasketAsync(basketProducts);

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("client-account-orders");


            async Task ResetBasketAsync(List<BasketProduct> basketProducts)
            {
                await Task.Run(() => _dataContext.RemoveRange(basketProducts));
            }

            async Task CreateFullOrderProductAync(Order order, List<BasketProduct> basketProducts)
            {
                foreach (var item in basketProducts)
                {
                    var orderProduct = new OrderProduct
                    {
                        OrderId = order.Id,
                        ProductId = item.ProductId,
                        Price = item.Product!.Price,
                        Quantity = item.Quantity,
                        Total = item.Product.Price * item.Quantity,
                    };
                    await _dataContext.OrderProducts.AddAsync(orderProduct);
                    await _dataContext.SaveChangesAsync();
                    var product = await _dataContext.Products.FirstOrDefaultAsync(p => p.Id == orderProduct.ProductId);
                    var productCount = await _dataContext.ProductCounts.FirstAsync(p => p.Id == product!.Id);


                    var newCount = productCount.Count - orderProduct.Quantity;
                    if (newCount>=0)
                    {
                        productCount.Count = newCount;
                        
                    }
                    else
                    {
                        orderProduct.Quantity = Convert.ToInt32(productCount.Count);
                        productCount.Count = 0;
                    }

                    product!.SaleCount = orderProduct.Quantity;

                    await _dataContext.SaveChangesAsync();
                }             
                

            }

            async Task<Order> CreateOrder()
            {
                var order = new Order
                {
                    Id = await _orderService.GenerateUniqueTrackingCodeAsync(),
                    UserId = _userService.CurrentUser.Id,
                    Status = Database.Models.Enums.OrderStatus.Created
                };

                await _dataContext.Orders.AddAsync(order);
                return order;
            }
        }
        #endregion
    }
}
