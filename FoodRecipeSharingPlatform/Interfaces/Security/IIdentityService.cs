namespace FoodRecipeSharingPlatform.Interfaces.Security;
public interface IIdentityService
{
    string? GetUserId();
    string GetUserName();
}