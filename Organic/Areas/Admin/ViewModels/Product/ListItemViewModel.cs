

using Organic.Areas.Admin.ViewModels.Category;

namespace Organic.Areas.Admin.ViewModels.Product
{
    public class ListItemViewModel
    {
        public ListItemViewModel(int id, string? name, string? info, decimal price, decimal count, int rate, DateTime createdAt, List<CategoryViewModel>? categories)
        {
            Id = id;
            Name = name;
            Info = info;
            Price = price;
            Count = count;
            Rate = rate;
            CreatedAt = createdAt;
            Categories = categories;
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Info { get; set; }
        public decimal Price { get; set; }
        public decimal Count { get; set; }
        public int Rate { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<CategoryViewModel>? Categories { get; set; }

    }
}
