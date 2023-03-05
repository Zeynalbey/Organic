using Organic.Database.Models.Common;

namespace Organic.Database.Models
{
    public class BlogAndCategory : BaseEntity<int>
    {
        public int BlogId { get; set; }
        public Blog? Blog { get; set; }

        public int BlogCategoryId { get; set; }
        public BlogCategory? BlogCategory { get; set; }
    }
}
