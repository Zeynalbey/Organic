

namespace Organic.Areas.Client.ViewModels.Blog
{
    public class BlogDetailViewModel
    {
        public BlogDetailViewModel(int id, string title, string description, DateTime postedDate, string image, string from, string category)
        {
            Id = id;
            Title = title;
            Description = description;
            PostedDate = postedDate;
            Image = image;
            From = from;
            Category = category;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        //public string From { get; set; }
        public DateTime PostedDate { get; set; }
        public string Image { get; set; }
        public string From { get; set; }
        public string Category { get; set; }

        public class CategoryViewModeL
        {
            public CategoryViewModeL(string title)
            {
                Title = title;
            }

            public string Title { get; set; }

        }


            //public List<LikeListViewModel>? Likes { get; set; }
            //public List<CommentListViewModel>? Comments { get; set; }

            //public class CommentListViewModel
            //{
            //    public CommentListViewModel(int id, DateTime commentDate,
            //        string? text, string? from)
            //    {
            //        Id = id;
            //        CommentDate = commentDate;
            //        Text = text;
            //        From = from;
            //    }

            //    public int Id { get; set; }
            //    public DateTime CommentDate { get; set; }
            //    public string? Text { get; set; }
            //    public string? From { get; set; }
            //}

            //public class LikeListViewModel
            //{
            //    public LikeListViewModel(int id, int likeCount)
            //    {
            //        Id = id;
            //        LikeCount = likeCount;
            //    }

            //    public int Id { get; set; }
            //    public int LikeCount { get; set; }
            //}

        }


    }
