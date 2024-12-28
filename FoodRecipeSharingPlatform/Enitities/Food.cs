using FoodRecipeSharingPlatform.Enitities.Identity;

namespace FoodRecipeSharingPlatform.Enitities;

public class Food : BaseEntity
{
    public string Name { get; set; } = null!;
    public string? Image { get; set; }
    public string? Description { get; set; }
    public string? AverageStar { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public List<FoodIngredient>? FoodIngredients { get; set; }
    public ICollection<Rating>? Ratings { get; set; }
    public List<Step>? Steps { get; set; }
}