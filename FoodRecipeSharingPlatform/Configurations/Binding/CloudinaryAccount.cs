namespace FoodRecipeSharingPlatform.Configurations.Binding;

public sealed class CloudinaryAccount
{
    public const string SectionName = "CloudinarySettings";
    public string CloudName { get; set; } = null!;
    public string ApiKey { get; set; } = null!;
    public string ApiSecret { get; set; } = null!;
    public string CloudinaryUrl { get; set; } = null!;
}