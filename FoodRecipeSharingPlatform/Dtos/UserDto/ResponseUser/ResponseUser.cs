namespace FoodRecipeSharingPlatform.Dtos.UserDto.ResponseUser;

public record ResponseUser(
    string UserName,
    string Email,
    string PhoneNumber,
    string FullName);