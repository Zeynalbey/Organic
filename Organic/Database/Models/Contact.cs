using Organic.Database.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace Organic.Database.Models
{
    public class Contact : BaseEntity<int>
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Message { get; set; }    
        public DateTime CreatedAt { get; set; }
    }
}
