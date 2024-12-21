namespace FoodRecipeSharingPlatform.Data.Common;
using System.Reflection;
using FoodRecipeSharingPlatform.Enitities;
using FoodRecipeSharingPlatform.Enitities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<User, Role, Guid>(options)
{
    public required new DbSet<User> Users { get; set; }
    public required DbSet<Food> Foods { get; set; }
    public required DbSet<Rating> Ratings { get; set; }
    public required DbSet<Ingredient> Ingredients { get; set; }
    public required DbSet<FoodIngredient> FoodIngredients { get; set; }
    public required DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        foreach (var entity in builder.Model.GetEntityTypes())
        {
            entity.SetTableName(entity.DisplayName());
        }
    }
}