using Organic.Database;
using Microsoft.EntityFrameworkCore;
using Organic.Services.Abstracts;
using Organic.Services.Concretes;

namespace Organic.Infrastructure.Configurations
{
    public static class RegisterCustomServicesConfigurations
    {
        public static void RegisterCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IEmailService, SMTPService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddSingleton<IFileService, FileService>();
            services.AddScoped<IUserActivationService, UserActivationService>();
            //services.AddScoped<IOrderService, OrderService>();
            //services.AddScoped<INotificationService, NotificationService>();
        }
    }
}
