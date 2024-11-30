using FoodRecipeSharingPlatform.Enitities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodRecipeSharingPlatform.Data.Configuration;

public class FoodIngredientConfig : IEntityTypeConfiguration<FoodIngredient>
{
    public void Configure(EntityTypeBuilder<FoodIngredient> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.HasOne(x => x.Food)
            .WithMany(x => x.FoodIngredients)
            .HasForeignKey(x => x.FoodId);

        builder.HasOne(x => x.Ingredient)
            .WithMany(x => x.FoodIngredients)
            .HasForeignKey(x => x.IngredientId);
    }
}