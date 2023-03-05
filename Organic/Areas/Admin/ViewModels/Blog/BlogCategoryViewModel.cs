namespace Organic.Areas.Admin.ViewModels.Blog
{
    public class BlogCategoryViewModel
    {
        public int Id { get; set; }
        public string BlogCategoryName { get; set; }

        public BlogCategoryViewModel(int id, string blogCategoryName)
        {
            Id = id;
            BlogCategoryName = blogCategoryName;
        }
    }
}
