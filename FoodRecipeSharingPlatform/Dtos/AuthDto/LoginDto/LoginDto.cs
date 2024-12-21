namespace FoodRecipeSharingPlatform.Dtos.AuthDto.LoginDto;

public record LoginDto(
    string UserNameOrEmail,
    string Password,
    bool RememberMe
);