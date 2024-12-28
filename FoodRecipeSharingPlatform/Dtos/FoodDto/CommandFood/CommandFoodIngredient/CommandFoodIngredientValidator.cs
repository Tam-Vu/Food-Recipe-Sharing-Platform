using FluentValidation;

namespace FoodRecipeSharingPlatform.Dtos.FoodDto.CommandFood.CommandFoodIngredient;

public class CommandFoodIngredientValidator : AbstractValidator<CommandFoodIngredients>
{
    public CommandFoodIngredientValidator()
    {
        RuleFor(x => x.IngredientId).NotEmpty();
        RuleFor(x => x.Quantity).NotEmpty();
    }
}