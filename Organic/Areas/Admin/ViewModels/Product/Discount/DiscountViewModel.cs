namespace Organic.Areas.Admin.ViewModels.Product.Discount
{
    public class DiscountViewModel
    {
        public DiscountViewModel(int id, decimal percent)
        {
            Id = id;
            Percent = percent;
        }

        public int Id { get; set; }
        public decimal Percent { get; set; }
    }
}
