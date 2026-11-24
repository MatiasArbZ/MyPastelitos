using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MyPastelitos.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.AddCustomConfiguration();

WebApplication app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
 
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.UseEndpoints(endpoints =>
{

    endpoints.MapControllers();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
        .WithStaticAssets();

    endpoints.MapGet("/api/health", () =>
    {
        return "Api is working!";
    });
});


app.AddCustomWebApplicationConfiguration();

app.Run();
