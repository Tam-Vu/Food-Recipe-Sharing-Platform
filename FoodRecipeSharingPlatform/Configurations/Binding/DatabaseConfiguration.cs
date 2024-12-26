namespace FoodRecipeSharingPlatform.Configurations.Binding;

public sealed class DatabaseConfiguration
{
    public const string dataConfig = "DatabaseConfiguration";
    public string RedisConnectionString { get; set; } = null!;
    public string ConnectionString { get; set; } = null!;
}