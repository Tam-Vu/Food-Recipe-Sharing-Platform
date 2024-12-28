using FoodRecipeSharingPlatform.Dtos.FoodDto.CommandFood.CommandFoodIngredient;
using FoodRecipeSharingPlatform.Dtos.FoodDto.CommandFood.CommandStep;

namespace FoodRecipeSharingPlatform.Dtos.FoodDto.CommandFood.CommandFood;

public record CommandFood
(
    string Name,
    string? Image,
    string? Description,
    Guid CategoryId,
    IEnumerable<CommandFoodIngredients> CommandFoodIngredient,
    IEnumerable<CommandSteps> CommandStep
);