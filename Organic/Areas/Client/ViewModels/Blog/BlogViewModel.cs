using Organic.Areas.Admin.ViewModels.Blog;

namespace Organic.Areas.Client.ViewModels.Blog
{
    public class BlogViewModel
    {
        public BlogViewModel(int id, string? title, string? description,
            string? from, string fromİmage, DateTime postedDate, string image, List<BlogCategoryViewModel> categories, int likes, int comments)
        {
            Id = id;
            Title = title;
            Description = description;
            From = from;
            Fromİmage = fromİmage;
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
        public string? Fromİmage { get; set; }
        public DateTime PostedDate { get; set; }
        public string Image { get; set; }
        public List<BlogCategoryViewModel> Categories { get; set; }
        public int Likes { get; set; }
        public int Comments { get; set; }
    }
}
