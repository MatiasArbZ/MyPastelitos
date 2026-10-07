using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using Microsoft.EntityFrameworkCore;
using MyPastelitos.Web.Data;
using MyPastelitos.Web.Services.Abstractions;
using MyPastelitos.Web.Services.Implementations;

namespace MyPastelitos.Web
{
    public static class CustomConfiguration
    {
        public static WebApplicationBuilder AddCustomConfiguration(this WebApplicationBuilder builder)
        {
            // Data Context
            builder.Services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection"));
            });

            // AutoMapper
            builder.Services.AddAutoMapper(typeof(Program));

            // Services

            // Toast Notifications
            builder.Services.AddNotyf(config =>
            {
                config.DurationInSeconds = 10;
                config.Position = NotyfPosition.BottomRight;
                config.IsDismissable = true;
            });


            // AddServices(builder);

            return builder;

        }

        private static void AddServices(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ISectionsService, SectionsService>();
        }

        public static WebApplication AddCustomWebApplicationConfiguration(this WebApplication app)
        {
           app.UseNotyf();
            return app;
        }



    }
}