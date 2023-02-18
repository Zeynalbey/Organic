namespace Organic.Areas.Admin.ViewModels.Slider
{
    public class ListViewModel
    {
        public ListViewModel(int id, string productName, string title,
            string description, string shopButtonName, string shopButtonUrl,
            string detailButton, string detailButtonUrl, string image, 
            DateTime createdAt)
        {
            Id = id;
            ProductName = productName;
            Title = title;
            Description = description;
            ShopButtonName = shopButtonName;
            ShopButtonUrl = shopButtonUrl;
            DetailButton = detailButton;
            DetailButtonUrl = detailButtonUrl;
            Image = image;
            CreatedAt = createdAt;
        }

        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ShopButtonName { get; set; }
        public string ShopButtonUrl { get; set; }
        public string DetailButton { get; set; }
        public string DetailButtonUrl { get; set; }
        public string Image { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
