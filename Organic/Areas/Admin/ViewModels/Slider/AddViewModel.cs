using Organic.Areas.Admin.ViewModels.Product;
using System.ComponentModel.DataAnnotations;

namespace Organic.Areas.Admin.ViewModels.Slider
{
    public class AddViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<ProductViewModel>? Products { get; set; }
    }
}
