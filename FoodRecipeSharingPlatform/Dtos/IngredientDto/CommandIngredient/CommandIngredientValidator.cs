using FluentValidation;

namespace FoodRecipeSharingPlatform.Dtos.IngredientDto.CommandIngredient;

public class CommandGredientValidator : AbstractValidator<CommandIngredient>
{
    public CommandGredientValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(50).WithMessage("Name must not exceed 50 characters");
    }
}