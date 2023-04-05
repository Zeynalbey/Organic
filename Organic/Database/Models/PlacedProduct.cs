namespace Organic.Database.Models
{
    public class PlacedProduct
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public decimal Count { get; set; }
    }
}
