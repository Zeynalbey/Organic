using Organic.Areas.Client.ViewModels.Authentication;
using Organic.Areas.Client.ViewModels.Basket;
using Organic.Database.Models;

namespace Organic.Services.Abstracts
{
    public interface IBasketService
    {
        Task<List<ProductCookieViewModel>> AddBasketProductAsync(Product product);
    }
}
