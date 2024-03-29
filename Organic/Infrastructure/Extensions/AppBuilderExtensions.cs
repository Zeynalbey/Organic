﻿
namespace Organic.Infrastructure.Extensions
{
    public static class AppBuilderExtensions
    {
        public static void ConfigureMiddlewarePipeline(this WebApplication app)
        {
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStatusCodePagesWithRedirects("/Error/Index");

            app.MapControllerRoute(
                name: "default",
                pattern: "{area=exists}/{controller=home}/{action=index}");

        }
    }
}
