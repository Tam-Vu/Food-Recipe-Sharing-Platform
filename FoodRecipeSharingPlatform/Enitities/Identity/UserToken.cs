using Microsoft.AspNetCore.Identity;

namespace FoodRecipeSharingPlatform.Enitities.Identity
{
    public class UserToken
    {
        public string UserId { get; set; } = null!;
        public string LoginProvider { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Value { get; set; } = null!;
    }
}