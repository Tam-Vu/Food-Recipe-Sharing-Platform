using FluentValidation;

namespace FoodRecipeSharingPlatform.Dtos.CategoryDto.CommandCategory;

public class CommandCategoryValidator : AbstractValidator<CommandCategory>
{
    public CommandCategoryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(50).WithMessage("Name must not exceed 50 characters");
    }
}