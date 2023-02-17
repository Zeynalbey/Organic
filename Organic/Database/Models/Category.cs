using Organic.Database.Models.Common;

namespace Organic.Database.Models
{
    public class Category : BaseEntity<int>, IAuditable
    {
        public string? Name { get; set; }
        public string? IconClass { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<Product>? Products { get; set; }
    }
}
