namespace FoodRecipeSharingPlatform.Configurations.Binding;

public sealed class JwtConfiguration
{
    public const string jwtConfig = "JwtSettings";
    public required string Secret { get; set; }
    public required string TokenExpirationInMinutes { get; set; }
    public required string Issuer { get; set; }
    public required string Audience { get; set; }
    public required string RefreshTokenExpirationInDays { get; set; }
}