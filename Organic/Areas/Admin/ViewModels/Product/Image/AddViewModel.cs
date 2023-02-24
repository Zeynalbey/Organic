using System.ComponentModel.DataAnnotations;

namespace Organic.Areas.Admin.ViewModels.Product.Image
{
    public class AddViewModel
    {
        public IFormFile? Image { get; set; }
    }
}
