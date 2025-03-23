using Microsoft.EntityFrameworkCore;

using PS256K.Data;

namespace PS256K.Extensions;

public static class AspNetCoreExtensions
{
    public static void ApplyMigrations(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var migrations = context.Database.GetPendingMigrations();

            if (!migrations.Any())
            {
                return;
            }

            context.Database.Migrate();
        }
    }


    public static void CreateDatabase(this WebApplication app)
    {
        if (!File.Exists("./PS256K.db"))
        {
            File.Create("./PS256K.db");
        }
    }
}
