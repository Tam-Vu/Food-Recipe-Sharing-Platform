using FoodRecipeSharingPlatform.Enitities.Identity;

namespace FoodRecipeSharingPlatform.Enitities;

public class Food : BaseEntity
{
    public required string Name { get; set; }
    public string? Image { get; set; }
    public string? Description { get; set; }
    public string? AverageStar { get; set; }
    public Guid CategoryId { get; set; }
    public required Category Category { get; set; }
    public Guid UserId { get; set; }
    public required User User { get; set; }
    public required ICollection<FoodIngredient> FoodIngredients { get; set; }
    public ICollection<Rating>? Ratings { get; set; }
    public required IEnumerable<Step> Steps { get; set; }
}