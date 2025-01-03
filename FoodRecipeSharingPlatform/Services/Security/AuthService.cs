using AutoMapper;
using FoodRecipeSharingPlatform.Dtos.AuthDto.ChangePasswordDto;
using FoodRecipeSharingPlatform.Dtos.AuthDto.ConfirmEmailDto;
using FoodRecipeSharingPlatform.Dtos.AuthDto.LoginDto;
using FoodRecipeSharingPlatform.Dtos.AuthDto.RegisterDto;
using FoodRecipeSharingPlatform.Dtos.AuthDto.ResetPasswordDto;
using FoodRecipeSharingPlatform.Enitities.Identity;
using FoodRecipeSharingPlatform.Entities.Models;
using FoodRecipeSharingPlatform.Enums;
using FoodRecipeSharingPlatform.Exceptions;
using FoodRecipeSharingPlatform.Interfaces;
using FoodRecipeSharingPlatform.Interfaces.Security;
using Microsoft.AspNetCore.Identity;
// using StackExchange.Redis;

namespace FoodRecipeSharingPlatform.Services.Security;

public class AuthService : IAuthService
{
    private readonly IJwtService _jwtSerivce;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IUserTokenRepository _userTokenRepository;
    private readonly IEmailSenderRepository _emailSenderRepository;
    // private readonly ConnectionMultiplexer _redis;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserServiceRepository _userServiceRepository;
    public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, IJwtService jwtSerivce,
        IMapper mapper, IUserTokenRepository userTokenRepository, IEmailSenderRepository emailSenderRepository,
        // ConnectionMultiplexer redis, 
        IUnitOfWork unitOfWork,
        IUserServiceRepository userServiceRepository)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtSerivce = jwtSerivce;
        _mapper = mapper;
        _userTokenRepository = userTokenRepository;
        _emailSenderRepository = emailSenderRepository;
        // _redis = redis;
        _unitOfWork = unitOfWork;
        _userServiceRepository = userServiceRepository;
    }

    public async Task<string> ChangePasswordAsync(ChangePasswordDto changePasswordDto)
    {
        try
        {
            string OldPass = changePasswordDto.CurrentPassword;
            string NewPass = changePasswordDto.NewPassword;
            var token = _jwtSerivce.GetCurrentToken();
            var user = await _userServiceRepository.GetUserByToken(token!);

            var ResultSignIn = await _signInManager.CheckPasswordSignInAsync(user!, OldPass, false);
            if (!ResultSignIn.Succeeded)
            {
                throw new BadRequestException("Current password is incorrect");
            }
            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user!);
            var ir = await _userManager.ResetPasswordAsync(user!, resetToken, NewPass);
            if (!ir.Succeeded)
            {
                throw new BadRequestException(ir.Errors.FirstOrDefault()?.Description!);
            }
            await LogoutAsync();
            return "Change password successfully";
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw new BadRequestException("Change password failed, please try later");
        }
    }

    public async Task<string> ConfirmEmailAsync(ConfirmEmailDto confirmEmailDto)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(confirmEmailDto.Email);
            if (user == null)
            {
                throw new BadRequestException("Email is not existed");
            }
            var result = await _userManager.ConfirmEmailAsync(user, confirmEmailDto.Token);
            if (!result.Succeeded)
            {
                throw new BadRequestException($"Confirm email failed: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
            return "Confirm email successfully";
        }

        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw new BadRequestException("Some errors occur, please try later");
        }
    }

    public async Task<string> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto, CancellationToken cancellationToken)
    {
        try
        {
            string email = forgotPasswordDto.Email;
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new BadRequestException("Email is not existed");
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            await _emailSenderRepository.SendForgotPasswordCode(email, token, cancellationToken = default);
            return "Please check your email to reset password";
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw new BadRequestException("Some errors occur, please try later");
        }
    }

    public async Task<LoginResponse> LoginAsync(LoginDto loginDto)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(loginDto.UserNameOrEmail);
            user ??= await _userManager.FindByNameAsync(loginDto.UserNameOrEmail);
            if (user == null)
            {
                throw new BadRequestException("Username or password is incorrect");
            }
            var resultSignIn = await _signInManager.PasswordSignInAsync(user, loginDto.Password, loginDto.RememberMe, true);
            if (resultSignIn?.Succeeded != true)
            {
                throw new BadRequestException("Username or password is incorrect");
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtSerivce.GenerateToken(user.Id, user.Email!, user.FullName, user.UserName!, roles.ToList());

            // var redis = _redis.GetDatabase();
            // await redis.StringSetAsync(user.Id.ToString(), token);
            IdentityResult res = await _userManager.SetAuthenticationTokenAsync(user, loginDto.LoginProvider, loginDto.UserNameOrEmail, token);
            if (res.Succeeded != true)
            {
                await _signInManager.SignOutAsync();
                throw new BadRequestException("Login failed");
            }
            if (!res.Succeeded)
            {
                //cant set token
                await _signInManager.SignOutAsync();
                throw new BadRequestException("Login failed");
            }

            var result = new LoginResponse(token);
            return result;
        }
        catch (Exception e)
        {
            throw new BadRequestException(e.Message);
        }
    }

    public async Task<string> LogoutAsync()
    {
        try
        {
            var token = _jwtSerivce.GetCurrentToken();
            await _userTokenRepository.DeleteAsync(x => x.Value == token, default);
            // var redis = _redis.GetDatabase();
            // await redis.KeyDeleteAsync(userId!.ToString());
            await _signInManager.SignOutAsync();
            return "logout successfully";
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw new BadRequestException("Some errors occurred, please try again later");
        }
    }

    public async Task<ResponseCommand> RegisterAsync(RegisterDto registerDto, CancellationToken cancellationToken)
    {
        try
        {
            _unitOfWork.CreateTransaction();
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
            _unitOfWork.Commit();
            return result;
        }
        catch (BadRequestException)
        {
            throw;
        }
        catch (Exception e)
        {
            _unitOfWork.Rollback();
            throw new BadRequestException(e.Message);
        }
        finally
        {
            _unitOfWork.Dispose();
        }
    }

    public async Task<string> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
            if (user == null)
            {
                throw new BadRequestException("Email is not existed");
            }
            var result = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.Password);
            if (!result.Succeeded)
            {
                throw new BadRequestException("Reset password failed, please try later");
            }
            return "Reset password successfully";
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw new BadRequestException("Reset password failed, please try later");
        }
    }
}