namespace FoodRecipeSharingPlatform.Dtos.AuthDto.LoginDto;
public record LoginResponse(
    string AccessToken,
    string RefreshToken
);
