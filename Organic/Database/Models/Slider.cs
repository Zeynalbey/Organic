using Organic.Database.Models.Common;

namespace Organic.Database.Models
{
    public class Slider : BaseEntity<int>, IAuditable
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ImageName { get; set; }
        public string? ImageNameInSystem { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public int ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
