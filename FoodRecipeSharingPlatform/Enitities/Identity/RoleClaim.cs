using Microsoft.AspNetCore.Identity;

namespace FoodRecipeSharingPlatform.Enitities.Identity
{
    public class RoleClaim : IdentityRoleClaim<Guid>
    {
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset LastModifiedAt { get; set; }
    }
}