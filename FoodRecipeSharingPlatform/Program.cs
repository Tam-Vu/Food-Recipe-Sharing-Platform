using System.Net.Mail;
using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using FoodRecipeSharingPlatform.Configurations.Binding;
using FoodRecipeSharingPlatform.Configurations.Common;
using FoodRecipeSharingPlatform.Data.Common;
using FoodRecipeSharingPlatform.Data.Interceptor;
using FoodRecipeSharingPlatform.Enitities.Identity;
using FoodRecipeSharingPlatform.Interfaces;
using FoodRecipeSharingPlatform.Interfaces.Security;
using FoodRecipeSharingPlatform.Middlewares;
using FoodRecipeSharingPlatform.Repositories;
using FoodRecipeSharingPlatform.Services.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

var emailConfig = new EmailSenderConfiguration();
configuration.GetSection(EmailSenderConfiguration.EmailSettingConfig).Bind(emailConfig);
var databaseConfig = new DatabaseConfiguration();
configuration.GetSection(DatabaseConfiguration.dataConfig).Bind(databaseConfig);

{
    builder.Services.AddScoped<AuditableEntityInterceptor>();

    builder.Services
        .AddDbContext<ApplicationDbContext>((provider, option) =>
        {
            option.AddInterceptors(provider.GetRequiredService<AuditableEntityInterceptor>());
            option.UseNpgsql(databaseConfig.ConnectionString, options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
        })
        .AddFluentEmail(emailConfig.Username)
        .AddSmtpSender(new SmtpClient(emailConfig.Server)
        {
            Port = emailConfig.Port,
            Credentials = new System.Net.NetworkCredential(emailConfig.Username, emailConfig.Password),
            EnableSsl = emailConfig.EnableSsl
        });

    builder.Services
        .AddIdentity<User, FoodRecipeSharingPlatform.Enitities.Identity.Role>(options =>
        {
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedEmail = true;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

    builder.Services
        .Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequiredLength = 8;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;
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
        .AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(databaseConfig.RedisConnectionString))
        .AddSingleton(TimeProvider.System)
        .AddScoped<IIngredientRepository, IngredientRepository>()
        .AddScoped<ICategoryRepository, CategoryRepository>()
        .AddScoped<IAuthService, AuthService>()
        .AddScoped<IEmailSenderRepository, EmailSenderRepository>()
        .AddScoped<IJwtService, JwtService>()
        .AddScoped<IUserTokenRepository, UserTokenRepository>()
        .AddTransient<DbInitializer>();

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

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    try
    {
        var dbInitializer = services.GetService<DbInitializer>();
        dbInitializer!.SeedingData().Wait();
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred creating the DB.");
    }
};
app.Run();
