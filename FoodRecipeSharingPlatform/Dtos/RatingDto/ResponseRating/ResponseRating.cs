namespace FoodRecipeSharingPlatform.Dtos.RatingDto.ResponseRating;

public class ResponseRating
{
    public Guid Id { get; set; }
    public int Star { get; set; }
    public string? Comment { get; set; }
    public Guid UserId { get; set; }
    public required string UserName { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset LastModifiedAt { get; set; }
    public ResponseRating() { }
}