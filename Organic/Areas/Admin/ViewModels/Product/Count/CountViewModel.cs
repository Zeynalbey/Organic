namespace Organic.Areas.Admin.ViewModels.Product.Count
{
    public class CountViewModel
    {
        public CountViewModel(int id, int count)
        {
            Id = id;
            Count = count;
        }

        public int Id { get; set; }
        public int Count { get; set; }
    }
}
