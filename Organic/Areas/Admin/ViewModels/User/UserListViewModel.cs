﻿namespace Organic.Areas.Admin.ViewModels.User
{
    public class UserListViewModel
    {
        public UserListViewModel(Guid id, string? firstName, string? lastName, string? email, string role, string password, string image)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Role = role;
            Password = password;
            Image = image;
        }

        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        public string Image { get; set; } 
    }


}