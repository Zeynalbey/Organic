using Organic.Database.Models.Common;

namespace Organic.Database.Models
{
    public class BlogComment : BaseEntity<int>
    {
        public DateTime CommentDate { get; set; }
        public string? Text { get; set; }
        public User? From { get; set; }
        public Blog? Blog { get; set; }
        public int BlogId { get; set; }
    }
}
