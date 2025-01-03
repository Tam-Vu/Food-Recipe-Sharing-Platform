using FluentValidation;

namespace FoodRecipeSharingPlatform.Dtos.AuthDto.RegisterDto;

public class RegisterDtoValidator : AbstractValidator<RegisterDto>
{
    public RegisterDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6);

        RuleFor(x => x.RetypePassword)
            .Equal(x => x.Password)
            .WithMessage("Password does not match");

        RuleFor(x => x.UserName).NotEmpty();

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .Matches(@"^\d{10}$").WithMessage("Phone number is not valid");

        RuleFor(x => x.FullName).NotEmpty();
    }
}