namespace Organic.Areas.Admin.ViewModels.Blog
{
    public class AddViewModel
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime PostedDate { get; set; }
        public IFormFile? Image { get; set; }
        public List<int> CategoryIds { get; set; }
        public List<BlogCategoryViewModel>? Categories { get; set; }
    }
}
