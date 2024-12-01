using System.Reflection;
using System.Runtime.CompilerServices;
using FoodRecipeSharingPlatform.Configurations;
using FoodRecipeSharingPlatform.Configurations.Binding;
using FoodRecipeSharingPlatform.Data.Common;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;
{
    builder.Services.AddDbContext<ApplicationDbContext>(option =>
    {
        var databaseConfiguration = configuration.GetSection("DatabaseConfiguration").Get<DatabaseConfiguration>();
        option.UseNpgsql(databaseConfiguration!.ConnectionString, options =>
        {
            options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
        });
    });
}
builder.Services.AddControllers();
var app = builder.Build();
{
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
}
app.Run();
