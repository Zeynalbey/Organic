using System.ComponentModel.DataAnnotations;

namespace Organic.Areas.Client.ViewModels.Authentication
{
    public class ForgotPasswordViewModel
    {
        [Required]
        public string? Email { get; set; }
    }
}
