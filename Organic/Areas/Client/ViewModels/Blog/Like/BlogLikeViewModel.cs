namespace Organic.Areas.Client.ViewModels.Blog.Like
{
    public class BlogLikeViewModel
    {
        public BlogLikeViewModel(int id, int likeCount)
        {
            Id = Id;
            LikeCount = likeCount;
        }

        public int Id { get; set; }
        public int LikeCount { get; set; }
    }
}
