namespace Organic.Areas.Admin.ViewModels.Product.Category
{
    public class CategoryProductViewModel
    {
        public CategoryProductViewModel(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
