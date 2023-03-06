using Organic.Areas.Admin.ViewModels.Product.Count;
using Organic.Areas.Admin.ViewModels.Product.Discount;
using Organic.Areas.Admin.ViewModels.Product.Tag;

namespace Organic.Areas.Client.ViewModels.Product
{
    public class ProductDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public decimal Rating { get; set; }
        public int RatingCount { get; set; }
        public decimal Price { get; set; }
        public IEnumerable<DiscountViewModel> Discounts { get; set; }
        public IEnumerable<ProductImageViewModel> ImageUrls { get; set; }
        public IEnumerable<TagViewModel> Tags { get; set; }
        public IEnumerable<CountViewModel> ProductCounts { get; set; }

        public ProductDetailViewModel(int id, string name, string info, decimal rating, int ratingCount,
            decimal price, IEnumerable<DiscountViewModel> discounts, IEnumerable<ProductImageViewModel> imageUrls,
            IEnumerable<TagViewModel> tags, IEnumerable<CountViewModel> productCounts) // Parametre ekleyin
        {
            Id = id;
            Name = name;
            Info = info;
            Rating = rating;
            RatingCount = ratingCount;
            Price = price;
            Discounts = discounts;
            ImageUrls = imageUrls;
            Tags = tags;
            ProductCounts = productCounts; // Değer atayın
        }
    }
    public class ProductImageViewModel
    {
        public int Id { get; set; }
        public string? ImageUrl { get; set; }
    }

}
