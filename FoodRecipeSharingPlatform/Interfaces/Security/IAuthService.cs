using FoodRecipeSharingPlatform.Dtos.AuthDto.ChangePasswordDto;
using FoodRecipeSharingPlatform.Dtos.AuthDto.LoginDto;
using FoodRecipeSharingPlatform.Dtos.AuthDto.RegisterDto;
using FoodRecipeSharingPlatform.Enitities.Identity;
using FoodRecipeSharingPlatform.Entities.Models;

namespace FoodRecipeSharingPlatform.Interfaces.Security;

public interface IAuthService
{
    Task<ResponseCommand> RegisterAsync(RegisterDto registerDto, CancellationToken cancellationToken);
    Task<LoginResponse> LoginAsync(LoginDto loginDto);
    Task<string> RefreshTokenAsync();
    Task<string> LogoutAsync();
    Task<ResponseCommand> ConfirmEmailAsync(User user, string token);
    Task<ResponseCommand> ForgotPasswordAsync(string email);
    Task<ResponseCommand> ChangePasswordAsync(ChangePasswordDto changePasswordDto, string password);
}