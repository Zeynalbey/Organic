
using System.ComponentModel.DataAnnotations;

namespace Organic.Areas.Client.ViewModels.Blog.Comment
{
    public class AddCommentViewModel
    {
        public string? Text { get; set; }
        public DateTime PostedDate { get; set; }
        public int BlogId { get; set; }
    }
}
