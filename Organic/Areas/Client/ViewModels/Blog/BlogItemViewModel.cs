namespace Organic.Areas.Client.ViewModels.Blog
{
    public class BlogItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string? From { get; set; }
        public DateTime PostedDate { get; set; }
        public string Image { get; set; }
        public List<BlogCommentItemViewModel> Comments { get; set; }
        public List<BlogLikeItemViewModel> Likes { get; set; }
    }

    public class BlogCommentItemViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
    }

    public class BlogLikeItemViewModel
    {
        public int Id { get; set; }
    }

}
