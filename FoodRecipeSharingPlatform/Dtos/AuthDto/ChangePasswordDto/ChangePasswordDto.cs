namespace FoodRecipeSharingPlatform.Dtos.AuthDto.ChangePasswordDto;

public record ChangePasswordDto(
    string CurrentPassword,
    string NewPassword,
    string RetypeNewPassword
);