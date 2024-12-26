namespace FoodRecipeSharingPlatform.Dtos.AuthDto.ResetPasswordDto;
public record ResetPasswordDto
(
    string Token,
    string Email,
    string Password,
    string RetypePassword
);