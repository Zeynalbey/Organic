

using Organic.Areas.Admin.ViewModels.Product.Count;
using Organic.Areas.Admin.ViewModels.Product.Rate;

namespace Organic.Areas.Admin.ViewModels.Product
{
    public class ListItemViewModel
    {
        public ListItemViewModel(int id, string? name, string? info, decimal price, 
            DateTime createdAt, int categoryId, string categoryname, 
            List<RateViewModel> rates, List<CountViewModel> counts)
        {
            Id = id;
            Name = name;
            Info = info;
            Price = price;
            CreatedAt = createdAt;
            CategoryId = categoryId;
            Categoryname = categoryname;
            Rates = rates;
            Counts = counts;
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Info { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CategoryId { get; set; } 
        public string Categoryname { get; set; }

        public List<RateViewModel> Rates { get; set; }
        public List<CountViewModel> Counts { get; set; }

    }
}
