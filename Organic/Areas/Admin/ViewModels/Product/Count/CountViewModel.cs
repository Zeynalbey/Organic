namespace Organic.Areas.Admin.ViewModels.Product.Count
{
    public class CountViewModel
    {
        public CountViewModel(int id, decimal count)
        {
            Id = id;
            Count = count;
        }

        public int Id { get; set; }
        public decimal Count { get; set; }
    }
}
