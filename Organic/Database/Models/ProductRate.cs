namespace Organic.Database.Models
{
    public class ProductRate
    {
        public int Id { get; set; }
        public Product? Product { get; set; }
        public int Rating { get; set; }
    }
}
