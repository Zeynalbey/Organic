using Organic.Areas.Admin.ViewModels.Product.Count;
using Organic.Areas.Admin.ViewModels.Product.Discount;

namespace Organic.Areas.Admin.ViewModels.Product
{
    public class ListItemViewModel
    {
        public ListItemViewModel(int id, string? name, string? info, decimal rating, int ratingCount, decimal price, 
            DateTime createdAt, int categoryId, string categoryname, 
            List<CountViewModel> counts, List<DiscountViewModel>percents)
        {
            Id = id;
            Name = name;
            Info = info;
            Rating = rating;
            RatingCount = ratingCount;
            Price = price;
            CreatedAt = createdAt;
            CategoryId = categoryId;
            Categoryname = categoryname;
            Counts = counts;
            Percents = percents;
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Info { get; set; }
        public decimal Rating { get; set; }
        public int RatingCount { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CategoryId { get; set; } 
        public string Categoryname { get; set; }
        public List<CountViewModel> Counts { get; set; }
        public List<DiscountViewModel> Percents { get; set; }

    }
}
