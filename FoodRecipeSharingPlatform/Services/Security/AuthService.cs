using AutoMapper;
using FoodRecipeSharingPlatform.Dtos.AuthDto.ChangePasswordDto;
using FoodRecipeSharingPlatform.Dtos.AuthDto.LoginDto;
using FoodRecipeSharingPlatform.Dtos.AuthDto.RegisterDto;
using FoodRecipeSharingPlatform.Enitities.Identity;
using FoodRecipeSharingPlatform.Entities.Models;
using FoodRecipeSharingPlatform.Enums;
using FoodRecipeSharingPlatform.Exceptions;
using FoodRecipeSharingPlatform.Interfaces;
using FoodRecipeSharingPlatform.Interfaces.Security;
using Microsoft.AspNetCore.Identity;

namespace FoodRecipeSharingPlatform.Services.Security;

public class AuthService : IAuthService
{
    private readonly IJwtService _jwtSerivce;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IUserTokenRepository _userTokenRepository;
    private readonly IEmailSenderRepository _emailSenderRepository;

    public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, IJwtService jwtSerivce, IMapper mapper, IUserTokenRepository userTokenRepository, IEmailSenderRepository emailSenderRepository)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtSerivce = jwtSerivce;
        _mapper = mapper;
        _userTokenRepository = userTokenRepository;
        _emailSenderRepository = emailSenderRepository;
    }

    public Task<ResponseCommand> ChangePasswordAsync(ChangePasswordDto changePasswordDto, string password)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseCommand> ConfirmEmailAsync(User user, string token)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseCommand> ForgotPasswordAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task<LoginResponse> LoginAsync(LoginDto loginDto)
    {
        throw new NotImplementedException();
    }

    public Task<string> LogoutAsync()
    {
        throw new NotImplementedException();
    }

    public Task<string> RefreshTokenAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<ResponseCommand> RegisterAsync(RegisterDto registerDto, CancellationToken cancellationToken)
    {
        try
        {
            var checkEmail = await _userManager.FindByEmailAsync(registerDto.Email);
            if (checkEmail != null)
            {
                throw new BadRequestException("Email is already existed");
            }
            var checkUserName = await _userManager.FindByNameAsync(registerDto.UserName);
            if (checkUserName != null)
            {
                throw new BadRequestException("Username is already existed");
            }
            var user = _mapper.Map<User>(registerDto);
            var resultCreate = await _userManager.CreateAsync(user, registerDto.Password);
            if (!resultCreate.Succeeded)
            {
                var errors = string.Join(", ", resultCreate.Errors.Select(e => $"{e.Code}: {e.Description}"));
                throw new BadRequestException($"Create account failed: {errors}");
            }
            var role = await _userManager.AddToRoleAsync(user, nameof(RoleEnum.User));
            if (!role.Succeeded)
            {
                throw new Exception("Failed to assign role: " + string.Join(", ", role.Errors.Select(e => e.Description)));
            }
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            await _emailSenderRepository.SendConfirmEmailCode(user.Email!, token, cancellationToken);
            var result = _mapper.Map<ResponseCommand>(user);
            return result;
        }
        catch (Exception e)
        {
            throw new BadRequestException(e.Message);
        }
    }
}