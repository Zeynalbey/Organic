namespace Organic.Areas.Admin.ViewModels.Product.Category
{
    public class CategoryViewModel
    {
        public CategoryViewModel(int id, string name, string iconClass)
        {
            Id = id;
            Name = name;
            IconClass = iconClass;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string IconClass { get; set; }
    }
}
