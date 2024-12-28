using FoodRecipeSharingPlatform.Enitities;
using FoodRecipeSharingPlatform.Interfaces.Builder;

namespace FoodRecipeSharingPlatform.Repositories.Builder;

public class FoodBuilder : IFoodBuilder
{
    public FoodBuilder()
    {
    }
    private readonly Food _food = new();
    public void SetDescription(string? description, string? image)
    {
        _food.Description = description;
        _food.Image = image;
    }

    public void SetFoodIngredients(IEnumerable<FoodIngredient> foodIngredients)
    {
        _food.FoodIngredients ??= [];
        _food.FoodIngredients!.AddRange(foodIngredients.ToList());
    }

    public void SetName(string name, Guid categoryId, Guid userId)
    {
        _food.Name = name;
        _food.CategoryId = categoryId;
        _food.UserId = userId;
    }

    public void SetSteps(IEnumerable<Step> steps)
    {
        _food.Steps ??= [];
        _food.Steps!.AddRange(steps.ToList());
    }

    public Food Build()
    {
        return _food;
    }
}