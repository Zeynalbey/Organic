namespace Organic.Areas.Admin.ViewModels.Authentication
{
    public class UserListViewModel
    {
        public UserListViewModel(string? firstName, string? lastName, string? email, string role)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Role = role;
        }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string Role { get; set; }
    }
}
