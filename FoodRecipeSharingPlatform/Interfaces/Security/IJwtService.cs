using System.Security.Claims;
using FoodRecipeSharingPlatform.Enitities.Identity;
using Microsoft.IdentityModel.Tokens;

namespace FoodRecipeSharingPlatform.Interfaces.Security;

public interface IJwtService
{
    string? GetCurrentToken();
    TokenValidationParameters GetJwtParams();
    ClaimsPrincipal ValidateToken(string token);
    Claim? GetClaim(string token, string claimType);
    string GenerateToken(User user, List<string> roles);
}