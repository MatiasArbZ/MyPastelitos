using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using AspNetCoreHero.ToastNotification.Notyf.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyPastelitos.Web.Controllers;
using MyPastelitos.Web.Data;
using MyPastelitos.Web.Data.Entities;
using MyPastelitos.Web.Data.Seeders;
using MyPastelitos.Web.Helpers.Abstractions;
using MyPastelitos.Web.Helpers.Implementations;
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


            builder.Services.AddHttpContextAccessor();
            // Services

            // Toast Notifications
            builder.Services.AddNotyf(config =>
            {
                config.DurationInSeconds = 10;
                config.Position = NotyfPosition.BottomRight;
                config.IsDismissable = true;
            });

            // IAM

            AddIAM(builder);

           

            AddServices(builder);

            return builder;

        }

        private static void AddServices(WebApplicationBuilder builder)
        {

            //services
            builder.Services.AddScoped<ISectionsService, SectionsService>(); 
            builder.Services.AddScoped<IRecipesService, RecipesService>();
            builder.Services.AddScoped<IUsersService, UsersService>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IRolesService, RolesService>();
            builder.Services.AddScoped<IHomeService, HomeService>();


            builder.Services.AddTransient<SeedDb>();

            builder.Services.AddScoped<ISelectListHelper, SelectListHelper>();
        }


        //IAM Identity And Access Managment
        private static void AddIAM(WebApplicationBuilder builder)
        {
            builder.Services.AddIdentity<User, IdentityRole>(config =>
            {
                config.User.RequireUniqueEmail = true;
                config.Password.RequireDigit = false;
                config.Password.RequiredUniqueChars = 0;
                config.Password.RequireLowercase = false;
                config.Password.RequireUppercase = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequiredLength = 4;

            }).AddEntityFrameworkStores<DataContext>()
              .AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "Auth";
                config.ExpireTimeSpan = TimeSpan.FromDays(100);
                config.LoginPath = "/Account/login";
                config.AccessDeniedPath = "/Error/403";

            });
        }

        public static WebApplication AddCustomWebApplicationConfiguration(this WebApplication app)
        {
           app.UseNotyf();

            SeedData(app);

            return app;
        }

        private static void SeedData(WebApplication app)
        {
            IServiceScopeFactory scopeFactory = app.Services.GetService<IServiceScopeFactory>();

            using IServiceScope scope = scopeFactory.CreateScope();
            SeedDb service = scope.ServiceProvider.GetService<SeedDb>();
            service.SeedAsync().Wait();
        }

    }
}