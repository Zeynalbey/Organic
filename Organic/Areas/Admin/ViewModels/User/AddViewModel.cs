using Organic.Database.Models;
using System.ComponentModel.DataAnnotations;

namespace Organic.Areas.Admin.ViewModels.User
{
    public class AddViewModel
    {
        public Guid? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int RoleId { get; set; }
        public List<RoleListViewModel>? Roles { get; set; }
    }

    public class RoleListViewModel
    {
        public RoleListViewModel(int id, string? name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
