namespace FoodRecipeSharingPlatform.Entities.Models;

public class ResponseCommand
{
    public Guid Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset LastModifiedAt { get; set; }
}
