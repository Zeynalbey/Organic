using System.ComponentModel.DataAnnotations;

namespace Organic.Areas.Client.ViewModels.Authentication
{
    public class NewPasswordViewModel
    {
        public string? Password { get; set; }

        [Required(ErrorMessage = "Şifrəni təkrar yazın.")]
        [Compare(nameof(Password), ErrorMessage = "Şifrələr uyğun gəlmir.")]
        public string? ConfirmPassword { get; set; }
    }
}
