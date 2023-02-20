namespace Organic.Areas.Admin.ViewModels.Slider
{
    public class IndexViewModel
    {
        public IndexViewModel(int id, int productId, string? productName, string? title, string? description, string? image)
        {
            Id = id;
            ProductId = productId;
            ProductName = productName;
            Title = title;
            Description = description;
            Image = image;
        }

        public int Id { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
    }
}
