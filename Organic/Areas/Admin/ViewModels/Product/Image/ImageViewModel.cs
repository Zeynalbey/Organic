namespace Organic.Areas.Admin.ViewModels.Product.Image
{
    public class ImageViewModel
    {
        public int ProductId { get; set; }
        public List<ListItem>? Images { get; set; }

        public class ListItem
        {
            public int Id { get; set; }
            public string? ImageUrL { get; set; }
        }
    }
}
