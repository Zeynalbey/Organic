using Organic.Database.Models.Common;

namespace Organic.Database.Models
{
    public class Blog : BaseEntity<int>
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public User? From { get; set; }
        public DateTime PostedDate { get; set; }
        public string? ImageName { get; set; }
        public string? ImageNameInSystem { get; set; }
        public List<BlogLike>? Likes { get; set; }
        public List<BlogComment>? Comments { get; set; }

    }
}
