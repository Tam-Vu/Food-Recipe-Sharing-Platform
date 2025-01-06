using System.Net.Mail;
using System.Reflection;
using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using FoodRecipeSharingPlatform.Configurations.Binding;
using FoodRecipeSharingPlatform.Configurations.Common;
using FoodRecipeSharingPlatform.Data.Common;
using FoodRecipeSharingPlatform.Data.Interceptor;
using FoodRecipeSharingPlatform.Enitities.Identity;
using FoodRecipeSharingPlatform.Interfaces;
using FoodRecipeSharingPlatform.Interfaces.Builder;
using FoodRecipeSharingPlatform.Interfaces.Security;
using FoodRecipeSharingPlatform.Middlewares;
using FoodRecipeSharingPlatform.Repositories;
using FoodRecipeSharingPlatform.Repositories.Builder;
using FoodRecipeSharingPlatform.Services.Common;
using FoodRecipeSharingPlatform.Services.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using Serilog;
using Serilog.Sinks.Slack;
using Serilog.Core;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

var emailConfig = new EmailSenderConfiguration();
configuration.GetSection(EmailSenderConfiguration.SectionName).Bind(emailConfig);
var databaseConfig = new DatabaseConfiguration();
configuration.GetSection(DatabaseConfiguration.SectionName).Bind(databaseConfig);
var jwtConfig = new JwtConfiguration();
configuration.GetSection(JwtConfiguration.SectionName).Bind(jwtConfig);
var cloudinaryConfig = new CloudinaryAccount();
configuration.GetSection(CloudinaryAccount.SectionName).Bind(cloudinaryConfig);
var JsonConfiguration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();
{
    builder.Services
        // .AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(databaseConfig.RedisConnectionString))
        .AddSingleton(jwtConfig)
        .AddSingleton(cloudinaryConfig)
        .AddHttpClient()
        .AddScoped<IRepositoryFactory, RepositoryFactory>()
        .AddScoped(typeof(IBaseRepository<,,>), typeof(BaseRepository<,,>))
        .AddTransient<IUnitOfWork, UnitOfWork>()
        .AddScoped<ICloudinaryService, CloudinaryService>()
        .AddScoped<IJwtService, JwtService>()
        .AddScoped<IAuthService, AuthService>()
        .AddSingleton(TimeProvider.System)
        .AddScoped<IIngredientRepository, IngredientRepository>()
        .AddScoped<ICategoryRepository, CategoryRepository>()
        .AddScoped<IFoodBuilder, FoodBuilder>()
        .AddScoped<IFoodRepository, FoodRepository>()
        .AddScoped<IEmailSenderRepository, EmailSenderRepository>()
        .AddScoped<IUserTokenRepository, UserTokenRepository>()
        .AddScoped<IUserServiceRepository, UserServiceRepository>()
        .AddScoped<IIdentityService, IdentityService>()
        .AddTransient<DbInitializer>();

    builder.Host.UseSerilog((context, configuration) =>
    {
        configuration.ReadFrom.Configuration(context.Configuration);
    });

    builder.Services
        .AddScoped<AuditableEntityInterceptor>();

    builder.Services
        .AddDbContext<ApplicationDbContext>((provider, option) =>
        {
            option.AddInterceptors(provider.GetRequiredService<AuditableEntityInterceptor>());
            option.UseNpgsql(databaseConfig.ConnectionString, options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
        });

    builder.Services
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
            // options.SignIn.RequireConfirmedEmail = true;
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
        .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
        .AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtConfig.Issuer,
                ValidAudience = jwtConfig.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Secret))
            };
        });

    builder.Services
        .AddEndpointsApiExplorer()
        .AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new() { Title = "Food Recipe Sharing Platform", Version = "v1" });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\".\r\n\r\n enter token"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
            options.CustomSchemaIds(type => type.ToString());
        })
    .AddAuthorization();

    builder.Services
        .AddExceptionHandler<GlobalExceptionHander>()
        .AddProblemDetails();
}

var app = builder.Build();
{
    app.UseHttpsRedirection();
    // app.UseMiddleware<>();
    app.UseSerilogRequestLogging();
    app.UseAuthorization();
    app.UseExceptionHandler();
    app.MapControllers();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Food Recipe Sharing Platform v1"));
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