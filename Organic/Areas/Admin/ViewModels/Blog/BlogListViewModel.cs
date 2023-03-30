using Organic.Database.Models;

namespace Organic.Areas.Admin.ViewModels.Blog
{
    public class BlogListViewModel
    {
        public BlogListViewModel(int id, string? title, string? description,
            string? from, DateTime postedDate, string image, List<BlogCategoryViewModel> categories, 
            int likes,
            List<BlogCommentListViewModel>? comments)
        {
            Id = id;
            Title = title;
            Description = description;
            From = from;
            PostedDate = postedDate;
            Image = image;
            Categories = categories;
            Likes = likes;
            Comments = comments;  
        }

        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? From { get; set; }
        public DateTime PostedDate { get; set; }
        public string Image { get; set; }
        public List<BlogCategoryViewModel> Categories { get; set; }
        public int Likes { get; set; }
        public List<BlogCommentListViewModel>? Comments { get; set; }

    }

    public class BlogCommentListViewModel
    {
        public BlogCommentListViewModel(int id, DateTime commentDate, 
            string? text, string? from, string? blog)
        {
            Id = id;
            CommentDate = commentDate;
            Text = text;
            From = from;
            Blog = blog;
        }

        public int Id { get; set; }
        public DateTime CommentDate { get; set; }
        public string? Text { get; set; }
        public string? From { get; set; }
        public string? Blog { get; set; }
    }
}
