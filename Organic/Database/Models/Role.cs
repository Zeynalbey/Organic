using Organic.Database.Models.Common;

namespace Organic.Database.Models
{
    public class Role : BaseEntity<int>, IAuditable
    {
        public string? Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<User>? Users { get; set; }
    }
}
