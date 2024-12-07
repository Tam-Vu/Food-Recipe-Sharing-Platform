using FoodRecipeSharingPlatform.Enitities.Identity;

namespace FoodRecipeSharingPlatform.Enitities
{
    public class Rating : BaseEntity
    {
        public int Star { get; set; }
        public string? Comment { get; set; }
        public required Guid UserId { get; set; }
        public required User User { get; set; }
        public required Guid FoodId { get; set; }
        public required Food Food { get; set; }
    }
}