namespace Organic.Areas.Admin.ViewModels.Slider
{
    public class ListViewModel
    {
        public ListViewModel(int id, string productName, string title,
            string description, string image, 
            DateTime createdAt)
        {
            Id = id;
            ProductName = productName;
            Title = title;
            Description = description;
            Image = image;
            CreatedAt = createdAt;
        }

        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
