using FoodRecipeSharingPlatform.Dtos.AuthDto.ChangePasswordDto;
using FoodRecipeSharingPlatform.Dtos.AuthDto.ConfirmEmailDto;
using FoodRecipeSharingPlatform.Dtos.AuthDto.LoginDto;
using FoodRecipeSharingPlatform.Dtos.AuthDto.RegisterDto;
using FoodRecipeSharingPlatform.Dtos.AuthDto.ResetPasswordDto;
using FoodRecipeSharingPlatform.Enitities.Identity;
using FoodRecipeSharingPlatform.Entities.Models;

namespace FoodRecipeSharingPlatform.Interfaces.Security;

public interface IAuthService
{
    Task<ResponseCommand> RegisterAsync(RegisterDto registerDto, CancellationToken cancellationToken);
    Task<LoginResponse> LoginAsync(LoginDto loginDto);
    Task<string> LogoutAsync();
    Task<string> ConfirmEmailAsync(ConfirmEmailDto confirmEmailDto);
    Task<string> ChangePasswordAsync(ChangePasswordDto changePasswordDto);
    Task<string> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto, CancellationToken cancellationToken);
    Task<string> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);

}