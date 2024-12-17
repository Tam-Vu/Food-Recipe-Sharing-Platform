using System.Security.Claims;
using FoodRecipeSharingPlatform.Interfaces.Security;

namespace FoodRecipeSharingPlatform.Services.Security;

public class IdentityService : IIdentityService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public IdentityService(IHttpContextAccessor httpContextAccessor)
    {
        this._httpContextAccessor = httpContextAccessor;
    }

    public string? GetUserId() => this.GetClaim(ClaimTypes.NameIdentifier);

    public string GetUserName() => this.GetClaim(ClaimTypes.Name);

    private string GetClaim(string key) => this._httpContextAccessor.HttpContext?.User?.FindFirst(key)?.Value ?? string.Empty;
}