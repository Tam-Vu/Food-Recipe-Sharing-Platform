using Microsoft.AspNetCore.Identity;

namespace FoodRecipeSharingPlatform.Entities.Identity;
public class UserRole : IdentityUserRole<Guid>
{
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset LastModifiedAt { get; set; }
}