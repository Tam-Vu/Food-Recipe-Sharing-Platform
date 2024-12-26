using FluentValidation;

namespace FoodRecipeSharingPlatform.Dtos.UserDto.CommnadUser;

public class CommandUserValidator : AbstractValidator<CommandUser>
{
    public CommandUserValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty();

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .Matches(@"^(\+[0-9]{9})$").WithMessage("Phone number must be in international format");

        RuleFor(x => x.FullName)
            .NotEmpty();
    }
}