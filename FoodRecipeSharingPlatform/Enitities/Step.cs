namespace FoodRecipeSharingPlatform.Enitities
{
    public class Step : BaseEntity
    {
        public int Order { get; set; }
        public required string Description { get; set; }
        public string? Note { get; set; }
        public Guid FoodId { get; set; }
    }
}