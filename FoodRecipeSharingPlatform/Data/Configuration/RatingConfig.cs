using FoodRecipeSharingPlatform.Enitities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodRecipeSharingPlatform.Data.Configuration;

public class RatingConfig : IEntityTypeConfiguration<Rating>
{
    public void Configure(EntityTypeBuilder<Rating> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.HasOne(x => x.User)
            .WithMany(x => x.Ratings)
            .HasForeignKey(x => x.UserId);
    }
}