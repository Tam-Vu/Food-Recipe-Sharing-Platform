namespace FoodRecipeSharingPlatform.Enitities
{
    public class Category : BaseEntity
    {
        public required string Name { get; set; }
        public ICollection<Food>? Foods { get; set; }
    }
}