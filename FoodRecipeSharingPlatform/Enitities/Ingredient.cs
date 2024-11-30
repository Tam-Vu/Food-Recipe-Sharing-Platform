namespace FoodRecipeSharingPlatform.Enitities
{
    public class Ingredient : BaseEntity
    {
        public required string Name { get; set; }
        public ICollection<FoodIngredient>? FoodIngredients { get; set; }
    }
}