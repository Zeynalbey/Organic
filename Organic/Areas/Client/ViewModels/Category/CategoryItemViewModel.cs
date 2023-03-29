namespace Organic.Areas.Client.ViewModels.Category
{
    public class CategoryItemViewModel
    {
        public CategoryItemViewModel(int id, string title)
        {
            Id = id;
            Title = title;
        }

        public int Id { get; set; }
        public string Title { get; set; }
    }
}
