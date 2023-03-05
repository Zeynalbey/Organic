using Organic.Database.Models.Common;

namespace Organic.Database.Models
{
    public class BlogLike : BaseEntity<int>
    {
        public Blog? Blog { get; set; }
        public int BlogId { get; set; }
        public int LikeCount { get; set; }
    }
}
