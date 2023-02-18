using Organic.Database.Models.Common;

namespace Organic.Database.Models
{
    public class Product : BaseEntity<int>, IAuditable
    {
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int Rate { get; set; }
        public int ProductCount { get; set; }
        public string? Info { get; set; }
        public bool IsDollar { get; set; } = false;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public int CategoryID { get; set; }
        public Category? Category { get; set; }

        public List<ProductTag>? ProductTags { get; set; }

        public List<ProductImage>? ProductImages { get; set; }

        public int SliderId { get; set; }
        public Slider? Slider { get; set; }


    }
}
