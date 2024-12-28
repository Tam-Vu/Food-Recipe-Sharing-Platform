using FoodRecipeSharingPlatform.Dtos.FoodDto.CommandFood.CommandFoodIngredient;
using FoodRecipeSharingPlatform.Dtos.FoodDto.CommandFood.CommandStep;
using FoodRecipeSharingPlatform.Enitities;

namespace FoodRecipeSharingPlatform.Interfaces.Builder;

public interface IFoodBuilder
{
    void SetName(string name, Guid categoryId, Guid userId);
    void SetDescription(string? description, string? image);
    void SetFoodIngredients(IEnumerable<FoodIngredient> foodIngredients);
    void SetSteps(IEnumerable<Step> steps);
    Food Build();
}