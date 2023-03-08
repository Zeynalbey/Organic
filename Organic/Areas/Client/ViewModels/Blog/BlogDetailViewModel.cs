using Organic.Areas.Admin.ViewModels.Product.Count;
using Organic.Areas.Admin.ViewModels.Product.Discount;
using Organic.Areas.Admin.ViewModels.Product.Tag;

namespace Organic.Areas.Client.ViewModels.Product
{
    public class BlogDetailViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string From { get; set; }
        public DateTime PostedDate { get; set; }
        public string Image { get; set; }
       
        public List<LikeListViewModel> Likes { get; set; }
        public List<CommentListViewModel> Comments { get; set; }

        public class CommentListViewModel
        {
            public CommentListViewModel(int id, DateTime commentDate,
                string? text, string? from)
            {
                Id = id;
                CommentDate = commentDate;
                Text = text;
                From = from;
            }

            public int Id { get; set; }
            public DateTime CommentDate { get; set; }
            public string? Text { get; set; }
            public string? From { get; set; }
        }

        public class LikeListViewModel
        {
            public LikeListViewModel(int id, int likeCount)
            {
                Id = id;
                LikeCount = likeCount;
            }

            public int Id { get; set; }
            public int LikeCount { get; set; }
        }

    }
   

}
