namespace FoodRecipeSharingPlatform.Configurations.Binding;

public sealed class JwtConfiguration
{
    public const string SectionName = "JwtSettings";
    public string Secret { get; set; } = null!;
    public string TokenExpirationInMinutes { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public string RefreshTokenExpirationInDays { get; set; } = null!;
}