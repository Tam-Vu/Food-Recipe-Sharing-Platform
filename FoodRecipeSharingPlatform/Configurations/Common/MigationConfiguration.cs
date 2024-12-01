using FoodRecipeSharingPlatform.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace FoodRecipeSharingPlatform.Configurations;
public static class MigrationConfiguration
{
    public static WebApplication MigrateDatabase(this WebApplication webApplication)
    {
        using (var scope = webApplication.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<ApplicationDbContext>();
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while migrating the database.");
            }
        }
        return webApplication;
    }
}