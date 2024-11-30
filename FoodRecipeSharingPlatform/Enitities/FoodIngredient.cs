namespace FoodRecipeSharingPlatform.Enitities
{
    public class FoodIngredient : BaseEntity
    {
        public Guid FoodId { get; set; }
        public Food? Food { get; set; }
        public Guid IngredientId { get; set; }
        public required Ingredient Ingredient { get; set; }
        public required string Quantity { get; set; }
    }
}