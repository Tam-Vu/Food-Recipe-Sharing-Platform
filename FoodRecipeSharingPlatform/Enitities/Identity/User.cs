using Microsoft.AspNetCore.Identity;

namespace FoodRecipeSharingPlatform.Enitities.Identity;

public class User : IdentityUser<Guid>
{
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset LastModifiedAt { get; set; }
    public ICollection<Food>? Foods { get; set; }
    public ICollection<Rating>? Ratings { get; set; }
    public required string FullName { get; set; }
}