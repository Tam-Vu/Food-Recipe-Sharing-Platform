using System.Security.Claims;
using FoodRecipeSharingPlatform.Enitities.Identity;
using FoodRecipeSharingPlatform.Interfaces.Security;
using Microsoft.IdentityModel.Tokens;

namespace FoodRecipeSharingPlatform.Services.Security;

public class JwtService : IJwtService
{
    private readonly IConfiguration _config;
    private IHttpContextAccessor _contextAccessor;
    public JwtService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
    {
        _config = configuration;
        _contextAccessor = httpContextAccessor;
    }
    public string GenerateToken(User user, List<string> roles)
    {
        throw new NotImplementedException();
    }

    public Claim? GetClaim(string token, string claimType)
    {
        throw new NotImplementedException();
    }

    public string? GetCurrentToken()
    {
        return _contextAccessor.HttpContext?.Request.Headers["Authorization"].ToString()?.Replace("Bearer ", "");
    }

    public TokenValidationParameters GetJwtParams()
    {
        throw new NotImplementedException();
    }

    public ClaimsPrincipal ValidateToken(string token)
    {
        throw new NotImplementedException();
    }
}