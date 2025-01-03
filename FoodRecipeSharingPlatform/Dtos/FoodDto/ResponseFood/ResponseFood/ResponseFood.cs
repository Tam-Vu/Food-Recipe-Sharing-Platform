using FoodRecipeSharingPlatform.Dtos.FoodDto.ResponseFood.ResponseFoodIngredient;
using FoodRecipeSharingPlatform.Dtos.FoodDto.ResponseFood.ResponseStep;

namespace FoodRecipeSharingPlatform.Dtos.FoodDto.ResponseFood.ResponseFood;

public record ResponseFood
(
    Guid Id,
    string Name,
    string? Image,
    string? Description,
    string? AverageStar,
    string Category,
    Guid UserId,
    string UserName,
    List<ResponseFoodIngredients>? Ingredients,
    List<ResponseSteps>? Steps
);