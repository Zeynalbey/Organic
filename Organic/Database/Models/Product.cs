using Organic.Database.Models.Common;

namespace Organic.Database.Models
{
    public class Product : BaseEntity<int>, IAuditable
    {
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? Info { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public int CategoryID { get; set; }
        public Category? Category { get; set; }

        public List<ProductTag>? ProductTags { get; set; }

        public List<ProductImage>? ProductImages { get; set; }

        public List<Slider>? Sliders { get; set; }

        public List<BasketProduct>? BasketProducts { get; set; }

        public List<ProductCount>? ProductCounts { get; set; }

        public List<ProductRate>? ProductRates { get; set; }
    }
}
