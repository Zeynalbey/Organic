using Organic.Areas.Admin.ViewModels.Product.Category;
using Organic.Areas.Admin.ViewModels.Product.Discount;

namespace Organic.Areas.Client.ViewModels.Product
{
    public class ProductSaleViewModel
    {
        public ProductSaleViewModel(int id, string? name, string? info, string categoryName, decimal rating, int ratingCount,  decimal price,
            List<DiscountViewModel> discountPrices, string? mainImgUrl)
        {
            Id = id;
            Name = name;
            Info = info;
            CategoryName = categoryName;
            Rating = rating;
            RatingCount = ratingCount;
            DiscountPrices = discountPrices;
            Price = price;
            MainImgUrl = mainImgUrl;
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Info { get; set; }
        public string? CategoryName { get; set; }
        public decimal Rating { get; set; }
        public int RatingCount { get; set; }
        public List <DiscountViewModel> DiscountPrices { get; set; }
        public decimal Price { get; set; }
        public string? MainImgUrl { get; set; }
    }
}
