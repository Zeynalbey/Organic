using Organic.Database.Models.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Organic.Database.Models
{
    public class UserLogo : BaseEntity<int>
    {
        //[ForeignKey("User")]
        public string? ImageName { get; set; }
        public string? ImageNameInFileSystem { get; set; }

        public Guid? UserId { get; set; }
        public User? User { get; set; }
    }
}
