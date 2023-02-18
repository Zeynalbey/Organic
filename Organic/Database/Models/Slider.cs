using Organic.Database.Models.Common;

namespace Organic.Database.Models
{
    public class Slider : BaseEntity<int>, IAuditable
    {
        public string? ProductName { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ShopButtonName { get; set; }
        public string? ShopButtonUrl { get; set; }
        public string? DetailButton { get; set; }
        public string? DetailButtonUrl { get; set; }
        public string? ImageName { get; set; }
        public string? ImageNameInSystem { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Product? Product { get; set; }

    }
}
