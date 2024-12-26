using FoodRecipeSharingPlatform.Dtos.AuthDto.ChangePasswordDto;
using FoodRecipeSharingPlatform.Dtos.AuthDto.ConfirmEmailDto;
using FoodRecipeSharingPlatform.Dtos.AuthDto.LoginDto;
using FoodRecipeSharingPlatform.Dtos.AuthDto.RegisterDto;
using FoodRecipeSharingPlatform.Dtos.AuthDto.ResetPasswordDto;
using FoodRecipeSharingPlatform.Enitities.Models;
using FoodRecipeSharingPlatform.Entities.Models;
using FoodRecipeSharingPlatform.Interfaces.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodRecipeSharingPlatform.Controllers;


[ApiController]
[Route("api/[Controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto, CancellationToken cancellationToken)
    {
        var result = await _authService.RegisterAsync(registerDto, cancellationToken);
        return Ok(Result<ResponseCommand>.CreatedSuccess(result));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var result = await _authService.LoginAsync(loginDto);
        return Ok(Result<LoginResponse>.CreatedSuccess(result));
    }
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        var result = await _authService.LogoutAsync();
        return Ok(Result<string>.CommonSuccess(result));
    }

    [HttpGet("confirm-email")]
    public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailDto confirmEmailDto)
    {
        var result = await _authService.ConfirmEmailAsync(confirmEmailDto);
        return Ok(Result<string>.CommonSuccess(result));
    }

    [Authorize]
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
    {
        var result = await _authService.ChangePasswordAsync(changePasswordDto);
        return Ok(Result<string>.CommonSuccess(result));
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto, CancellationToken cancellationToken)
    {
        var result = await _authService.ForgotPasswordAsync(forgotPasswordDto, cancellationToken);
        return Ok(Result<string>.CommonSuccess(result));
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
    {
        var result = await _authService.ResetPasswordAsync(resetPasswordDto);
        return Ok(Result<string>.CommonSuccess(result));
    }
}