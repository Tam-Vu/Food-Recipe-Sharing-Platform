namespace FoodRecipeSharingPlatform.Enitities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset LastModified { get; set; }
        public Guid UpdateBy { get; set; }
        public Guid LastModifiedBy { get; set; }
    }
}