namespace FoodRecipeSharingPlatform.Dtos.UserDto.CommnadUser;

public record CommandUser(
    string UserName,
    string Email,
    string PhoneNumber,
    string FullName
);
