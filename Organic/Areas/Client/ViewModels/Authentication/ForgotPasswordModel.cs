using System.ComponentModel.DataAnnotations;

namespace Organic.Areas.Client.ViewModels.Authentication
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
    }
}
