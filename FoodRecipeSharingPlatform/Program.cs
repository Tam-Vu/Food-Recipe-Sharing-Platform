using System.Reflection;
using FoodRecipeSharingPlatform.Configurations.Binding;
using FoodRecipeSharingPlatform.Data.Common;
using FoodRecipeSharingPlatform.Interfaces;
using FoodRecipeSharingPlatform.Middlewares;
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
    builder.Services
        .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
        .AddControllers();

    builder.Services
        .AddEndpointsApiExplorer()
        .AddSwaggerGen();

    builder.Services
        .AddScoped<IIngredientRepository, IngredientRepository>();

    builder.Services
        .AddExceptionHandler<GlobalExceptionHander>()
        .AddProblemDetails();

}

var app = builder.Build();
{
    app.UseHttpsRedirection();
    // app.UseMiddleware<>();
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FoodRecipeSharingPlatform v1"));
}
app.Run();
