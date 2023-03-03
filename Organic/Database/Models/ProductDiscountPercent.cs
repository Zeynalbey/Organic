using Organic.Database.Models.Common;

namespace Organic.Database.Models
{
    public class ProductDiscountPercent
    {
        public int Id { get; set; }
        public Product? Product { get; set; }
        public decimal Percent { get; set; }
    }
}
