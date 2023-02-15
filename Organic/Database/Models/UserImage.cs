using Organic.Database.Models.Common;

namespace Organic.Database.Models
{
    public class UserImage : BaseEntity<int>
    {
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public string? ImageName { get; set; }
        public string? ImageNameInFileSystem { get; set; }
    }
}
