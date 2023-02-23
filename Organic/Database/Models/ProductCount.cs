using Organic.Database.Models.Common;

namespace Organic.Database.Models
{
    public class ProductCount
    {
        public int Id { get; set; }
        public Product? Product { get; set; }
        public decimal Count { get; set; }
    }
}
