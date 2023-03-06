using Organic.Database.Models.Common;

namespace Organic.Database.Models
{
    public class BlogCategory : BaseEntity<int>
    {
        public string? Name { get; set; }
        public List<BlogAndCategory>? BlogAndCategories { get; set; }
    }
}
