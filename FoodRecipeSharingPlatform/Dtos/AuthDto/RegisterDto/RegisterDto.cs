namespace FoodRecipeSharingPlatform.Dtos.AuthDto.RegisterDto;

public record RegisterDto(
    string Email,
    string Password,
    string RetypePassword,
    string UserName,
    string PhoneNumber
);