using FluentValidation;

namespace FoodRecipeSharingPlatform.Dtos.FoodDto.CommandFood.CommandFood;
public class CommandFoodValidator : AbstractValidator<CommandFood>
{
    public CommandFoodValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters");

        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("Category is required");
    }
}