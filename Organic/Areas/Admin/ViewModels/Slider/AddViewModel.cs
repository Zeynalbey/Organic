using System.ComponentModel.DataAnnotations;

namespace Organic.Areas.Admin.ViewModels.Slider
{
    public class AddViewModel
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ShopButtonName { get; set; }
        public string? ShopButtonUrl { get; set; }
        public string? DetailButton { get; set; }
        public string? DetailButtonUrl { get; set; }
        public IFormFile Image { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
