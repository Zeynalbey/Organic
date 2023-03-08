using System.ComponentModel.DataAnnotations;

namespace Organic.Areas.Admin.ViewModels.User
{
    public class UpdateViewModel
    {
        public Guid? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        public int RoleId { get; set; }
        public List<RoleListViewModel>? Roles { get; set; }
        public IFormFile? Image { get; set; }
        public string? İmageUrl { get; set; }
    }
}
