using FoodRecipeSharingPlatform.Enitities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodRecipeSharingPlatform.Data.Configuration;

public class IngredientConfig : IEntityTypeConfiguration<Ingredient>
{
    public void Configure(EntityTypeBuilder<Ingredient> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
    }
}