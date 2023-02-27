using Organic.Areas.Client.ViewModels.Authentication;
using Organic.Database.Models;

namespace Organic.Services.Abstracts
{
    public interface IUserService
    {
        public bool IsAuthenticated { get; }
        public User CurrentUser { get; }
        Task<bool> CheckPasswordAsync(string? email, string? password);
        string GetCurrentUserFullName();
        string GetCurrentUserRollName();
        Task SignInAsync(Guid id, string? role = null);
        //Task<bool> CheckRoleAsync(int roleId);
        Task SignInAsync(string? email, string? password, string? role = null);
        Task CreateAsync(RegisterViewModel model);
        Task SignOutAsync();
        Task<bool> CheckEmailConfirmedAsync(string? email);

    }
}
