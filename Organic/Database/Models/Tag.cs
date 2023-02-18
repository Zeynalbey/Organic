using Organic.Database.Models.Common;

namespace Organic.Database.Models
{
    public class Tag : BaseEntity<int>, IAuditable
    {
        public string? Name { get; set; }
        public List<ProductTag>? ProductTags { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
