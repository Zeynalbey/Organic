using AspNetCore.IServiceCollection.AddIUrlHelper;
using Organic.Infrastructure.Configurations;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Organic.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(o =>
                {
                    o.Cookie.Name = "Identity";
                    o.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                    o.LoginPath = "/auth/login";
                    o.AccessDeniedPath = "/admin/auth/login";
                });

            services.AddAutoMapper(typeof(Program));

            services.AddHttpContextAccessor();

            services.AddUrlHelper();

            services.AddSignalR();

            services.ConfigureMvc();

            services.ConfigureDatabase(configuration);

            services.ConfigureOptions(configuration);

            services.ConfigureFluentValidatios(configuration);

            services.RegisterCustomServices(configuration);
        }
    }
}
