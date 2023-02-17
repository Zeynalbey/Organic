namespace Organic.Areas.Admin.ViewModels.User.UserImage
{
    public class UserImageViewModel
    {
        public Guid UserId { get; set; }
        public List<ListItem>? Images { get; set; }

        public class ListItem
        {
            public Guid Id { get; set; }
            public string? ImageUrL { get; set; }
        }
    }
}
