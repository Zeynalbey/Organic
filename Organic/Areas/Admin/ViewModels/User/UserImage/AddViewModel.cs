using System.ComponentModel.DataAnnotations;

namespace Organic.Areas.Admin.ViewModels.User.UserImage
{
    public class AddViewModel
    {
        public IFormFile? Image { get; set; }
    }
}
