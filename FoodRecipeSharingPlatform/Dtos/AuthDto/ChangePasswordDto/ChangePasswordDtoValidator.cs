using FluentValidation;

namespace FoodRecipeSharingPlatform.Dtos.AuthDto.ChangePasswordDto;

public class ChangePasswordDtoValidator : AbstractValidator<ChangePasswordDto>
{
    public ChangePasswordDtoValidator()
    {
        RuleFor(x => x.CurrentPassword)
            .NotEmpty().WithMessage("Current password is required")
            .MinimumLength(6).WithMessage("Current password must be at least 6 characters");

        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage("New password is required")
            .MinimumLength(6).WithMessage("New password must be at least 6 characters");

        RuleFor(x => x.RetypeNewPassword)
            .NotEmpty().WithMessage("Retype new password is required")
            .Equal(x => x.NewPassword).WithMessage("Passwords do not match");
    }
}