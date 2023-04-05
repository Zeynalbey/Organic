
using System.ComponentModel.DataAnnotations;

namespace Organic.Areas.Admin.ViewModels.Product
{
    public class UpdateProductViewModel 
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? Info { get; set; }
        public decimal Count { get; set; }
        public decimal Percent { get; set; }
        [Required]
        public List<int>? TagIds { get; set; }
        public int CategoryId { get; set; }
        public List<CategoryListViewModel>? Categories { get; set; }
        public List<TagListViewModel>? Tags { get; set; }
    }
}
