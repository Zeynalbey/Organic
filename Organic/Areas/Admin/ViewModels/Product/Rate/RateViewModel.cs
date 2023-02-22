namespace Organic.Areas.Admin.ViewModels.Product.Rate
{
    public class RateViewModel
    {
        public RateViewModel(int id, int rate)
        {
            Id = id;
            Rate = rate;
        }

        public int Id { get; set; }
        public int Rate { get; set; }
    }
}
