using FoodRecipeSharingPlatform.Dtos.CategoryDto.ResponseCategory;
using FoodRecipeSharingPlatform.Dtos.FoodDto.ResponseFood.ResponseFoodIngredient;
using FoodRecipeSharingPlatform.Dtos.FoodDto.ResponseFood.ResponseStep;

namespace FoodRecipeSharingPlatform.Dtos.FoodDto.ResponseFood.ResponseFood;
public class ResponseFood
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Image { get; set; }
    public string? Description { get; set; }
    public string? AverageStar { get; set; }
    public string Category { get; set; } = null!;
    public Guid UserId { get; set; }
    public string FullName { get; set; } = null!;
    public List<ResponseFoodIngredients>? Ingredients { get; set; }
    public List<ResponseSteps>? Steps { get; set; }
    public ResponseFood()
    { }
}