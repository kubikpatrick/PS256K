using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using PS256K.Data;
using PS256K.Models.Identity;

namespace PS256K;

public sealed class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();
        builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddIdentity<User, IdentityRole>(options => 
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 6;
        })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }
        
        app.UseHttpsRedirection();
        app.UseStatusCodePages(async context => 
        {
            if (context.HttpContext.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                context.HttpContext.Response.Redirect("/not-found", true);
            }
        });
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapStaticAssets();
        app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}").WithStaticAssets();

        app.Run();
    }
}
