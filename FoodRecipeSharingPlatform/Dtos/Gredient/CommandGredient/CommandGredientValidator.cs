using FluentValidation;

namespace FoodRecipeSharingPlatform.Dtos.Gredient.CommandGredient;

public class CommandGredientValidator : AbstractValidator<CommandGredient>
{
    public CommandGredientValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(50).WithMessage("Name must not exceed 50 characters");
    }
}