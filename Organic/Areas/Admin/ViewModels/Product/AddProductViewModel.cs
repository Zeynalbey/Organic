using Organic.Areas.Admin.ViewModels.Product.Category;

namespace Organic.Areas.Admin.ViewModels.Product
{
    public class AddProductViewModel
    {
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? Info { get; set; }
        public int Count { get; set; }
        public List<int>? TagIds { get; set; }
        public List<int>? CategoryIds { get; set; }
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
