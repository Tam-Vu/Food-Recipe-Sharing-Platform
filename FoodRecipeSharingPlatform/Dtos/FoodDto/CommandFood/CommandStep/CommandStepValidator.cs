using FluentValidation;

namespace FoodRecipeSharingPlatform.Dtos.FoodDto.CommandFood.CommandStep;

public class CommandStepValidator : AbstractValidator<CommandSteps>
{
    public CommandStepValidator()
    {
        RuleFor(x => x.Order)
            .NotEmpty().WithMessage("Order is required")
            .GreaterThan(0).WithMessage("Order must be greater than 0");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required");

        RuleFor(x => x.Note)
            .NotEmpty().WithMessage("Note is required");
    }
}