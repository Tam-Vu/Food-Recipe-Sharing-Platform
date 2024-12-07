using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using FoodRecipeSharingPlatform.Configurations.Binding;
using FoodRecipeSharingPlatform.Configurations.Common;
using FoodRecipeSharingPlatform.Data.Common;
using FoodRecipeSharingPlatform.Enitities;
using FoodRecipeSharingPlatform.Interfaces;
using FoodRecipeSharingPlatform.Middlewares;
using FoodRecipeSharingPlatform.Repositories;
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
        .AddFluentValidationAutoValidation()
        .AddFluentValidationClientsideAdapters()
        .AddControllers(config => config.Filters.Add(typeof(ValidateDtoAttribute)));

    builder.Services
        .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

    builder.Services
        .AddEndpointsApiExplorer()
        .AddSwaggerGen();


    builder.Services
        .AddScoped<IRepositoryFactory, RepositoryFactory>()
        .AddScoped(typeof(IBaseRepository<,>), typeof(BaseRepository<,>))
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
    app.UseExceptionHandler();
    app.MapControllers();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FoodRecipeSharingPlatform v1"));
}
app.Run();
