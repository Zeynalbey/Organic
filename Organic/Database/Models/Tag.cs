using Organic.Database.Models.Common;

namespace Organic.Database.Models
{
    public class Tag : BaseEntity<int>
    {
        public string? Name { get; set; }
        public List<ProductTag>? ProductTags { get; set; }
    }
}
