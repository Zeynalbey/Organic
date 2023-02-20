using Organic.Areas.Admin.ViewModels.Product.Category;

namespace Organic.Areas.Client.ViewModels.Product
{
    public class ShopViewModel
    {
        public ShopViewModel(int id, string? name, string? info, decimal price)
        {
            Id = id;
            Name = name;
            Info = info;
            Price = price;
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Info { get; set; }
        public decimal Price { get; set; }
    }
}
