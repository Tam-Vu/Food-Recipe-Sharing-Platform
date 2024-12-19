using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FoodRecipeSharingPlatform.Configurations.Binding;
using FoodRecipeSharingPlatform.Interfaces.Security;
using Microsoft.IdentityModel.Tokens;

namespace FoodRecipeSharingPlatform.Services.Security;

public class JwtService : IJwtService
{
    private readonly JwtConfiguration _jwtConfiguration;
    private IHttpContextAccessor _contextAccessor;
    public JwtService(JwtConfiguration jwtConfiguration, IHttpContextAccessor httpContextAccessor)
    {
        _jwtConfiguration = jwtConfiguration;
        _contextAccessor = httpContextAccessor;
    }
    public string GenerateToken(Guid guid, string Email, string FullName, string UserName, List<string> roles)
    {
        var claims = new[]
{
                new Claim(ClaimTypes.NameIdentifier, guid.ToString()),
                new Claim(ClaimTypes.Email, Email),
                new Claim("FullName",FullName),
                new Claim(ClaimTypes.Role, string.Join(";",roles)),
                new Claim(ClaimTypes.Name, UserName)
            };

        var JwtSecurityKey = _jwtConfiguration.Secret;
        var JwtExpiryInHours = Convert.ToInt32(_jwtConfiguration.TokenExpirationInMinutes);
        var JwtIssuer = _jwtConfiguration.Issuer;
        var JwtAudience = _jwtConfiguration.Audience;

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSecurityKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var securityTokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddHours(JwtExpiryInHours),
            SigningCredentials = creds,
            Issuer = JwtIssuer,
            Audience = JwtAudience
        };
        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
        var token = jwtSecurityTokenHandler.WriteToken(securityToken);
        return token;
    }

    public Claim? GetClaim(string token, string claimType)
    {
        var cp = ValidateToken(token);
        return cp?.FindFirst(claimType);
    }

    public string? GetCurrentToken()
    {
        return _contextAccessor.HttpContext?.Request.Headers["Authorization"].ToString()?.Replace("Bearer ", "");
    }

    public TokenValidationParameters GetJwtParams()
    {
        TokenValidationParameters validationParameters = new();
        validationParameters.ValidateLifetime = true;
        validationParameters.ValidAudience = _jwtConfiguration.Audience;
        validationParameters.ValidIssuer = _jwtConfiguration.Issuer;
        validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.Secret));
        return validationParameters;
    }

    public ClaimsPrincipal? ValidateToken(string token)
    {
        try
        {
            TokenValidationParameters validationParameters = GetJwtParams();
            ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(token, validationParameters, out SecurityToken validatedToken);
            return principal;
        }
        catch (Exception e)
        {
            return null;
        }
    }
}