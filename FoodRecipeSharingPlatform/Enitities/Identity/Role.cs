using Microsoft.AspNetCore.Identity;
namespace FoodRecipeSharingPlatform.Enitities.Identity;

public class Role : IdentityRole<Guid>
{
    public Role() : base()
    {
    }

    public Role(string roleName) : base(roleName)
    {
    }
}
