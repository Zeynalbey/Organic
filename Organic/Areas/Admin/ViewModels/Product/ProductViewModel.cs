namespace Organic.Areas.Admin.ViewModels.Product
{
    public class ProductViewModel
    {
        public ProductViewModel(int id, string? name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
