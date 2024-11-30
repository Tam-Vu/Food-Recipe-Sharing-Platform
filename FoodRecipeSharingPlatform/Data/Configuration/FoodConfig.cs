using FoodRecipeSharingPlatform.Enitities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodRecipeSharingPlatform.Data.Configuration;
public class FoodConfig : IEntityTypeConfiguration<Food>
{
    public void Configure(EntityTypeBuilder<Food> builder)
    {
        builder.HasKey(f => f.Id);
        builder.Property(f => f.Id).ValueGeneratedOnAdd();
        builder.HasOne(f => f.Category)
            .WithMany(x => x.Foods)
            .HasForeignKey(f => f.CategoryId);

        builder.HasOne(f => f.User)
            .WithMany(x => x.Foods)
            .HasForeignKey(f => f.UserId);

        builder.HasMany(f => f.Ratings)
            .WithOne(x => x.Food)
            .HasForeignKey(f => f.FoodId);

        builder.OwnsMany(u => u.Steps, a =>
        {
            a.HasKey(x => x.Id);
            a.Property(x => x.Id).ValueGeneratedOnAdd();
        });
    }
}