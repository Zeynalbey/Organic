using Organic.Areas.Client.ViewModels.Product;

namespace Organic.Areas.Client.ViewModels.Category
{
    public class CategoryViewModel
    {
        public CategoryViewModel(int id, string name, string? iconClass, List<ProductViewModel> products)
        {
            Id = id;
            Name = name;
            IconClass = iconClass;
            Products = products;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string? IconClass { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}
