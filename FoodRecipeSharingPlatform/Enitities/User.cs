using Microsoft.AspNetCore.Identity;

namespace FoodRecipeSharingPlatform.Enitities;

public class User : IdentityUser<Guid>
{
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset LastModified { get; set; }
    public Guid UpdateBy { get; set; }
    public Guid LastModifiedBy { get; set; }
    public ICollection<Food>? Foods { get; set; }
    public ICollection<Rating>? Ratings { get; set; }
}