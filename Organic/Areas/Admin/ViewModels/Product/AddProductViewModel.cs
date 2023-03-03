using Organic.Areas.Admin.ViewModels.Product.Category;
using System.ComponentModel.DataAnnotations;

namespace Organic.Areas.Admin.ViewModels.Product
{
    public class AddProductViewModel
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string? Info { get; set; }
        [Required]
        public decimal Count { get; set; }
        public decimal Percent { get; set; }
        [Required]
        public List<int>? TagIds { get; set; }
        [Required]
        public int CategoryId { get; set; }

        public List<CategoryListViewModel>? Categories { get; set; }
        public List<TagListViewModel>? Tags { get; set; }


    }

    public class CategoryListViewModel
    {
        public CategoryListViewModel(int id, string? name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string? Name { get; set; }    
    }
    public class TagListViewModel
    {
        public TagListViewModel(int id, string? name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string? Name { get; set; }
    }

}
