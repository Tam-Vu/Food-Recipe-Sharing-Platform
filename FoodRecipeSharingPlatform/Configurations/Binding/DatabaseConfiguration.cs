namespace FoodRecipeSharingPlatform.Configurations.Binding;

public class DatabaseConfiguration
{
    public const string dataConfig = "DatabaseConfiguration";
    public required string ConnectionString { get; set; }
}