using Organic.Database.Models;

namespace Organic.Services.Abstracts
{
    public interface IUserActivationService
    {
        Task SendActivationUrlAsync(User user);
    }
}
