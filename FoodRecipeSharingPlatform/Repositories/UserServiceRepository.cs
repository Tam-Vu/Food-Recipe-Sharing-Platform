using System.Security.Claims;
using AutoMapper;
using FoodRecipeSharingPlatform.Dtos.UserDto.CommnadUser;
using FoodRecipeSharingPlatform.Dtos.UserDto.ResponseUser;
using FoodRecipeSharingPlatform.Enitities.Identity;
using FoodRecipeSharingPlatform.Entities.Models;
using FoodRecipeSharingPlatform.Exceptions;
using FoodRecipeSharingPlatform.Interfaces;
using FoodRecipeSharingPlatform.Interfaces.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FoodRecipeSharingPlatform.Repositories;

public class UserServiceRepository : IUserServiceRepository
{
    private readonly IBaseRepository<User, Guid, ResponseCommand> _userRepository;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    private readonly IJwtService _jwtService;
    private readonly ILogger<UserServiceRepository> _logger;
    public UserServiceRepository(IRepositoryFactory repositoryFactory, UserManager<User> userManager, IMapper mapper, IJwtService jwtService, ILogger<UserServiceRepository> logger)
    {
        _userRepository = repositoryFactory.GetRepository<User, Guid, ResponseCommand>();
        _userManager = userManager;
        _mapper = mapper;
        _jwtService = jwtService;
        _logger = logger;
    }

    public async Task<List<ResponseUser>> GetAllUser()
    {
        try
        {
            var users = await _userManager.Users.ToListAsync();
            var result = _mapper.Map<List<ResponseUser>>(users);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in {ClassName}, at {MethodName}: \nError: {Error} \nDetails: {Details}", nameof(UserServiceRepository), nameof(GetAllUser), ex.Message, ex.InnerException?.Message);
            throw new BadRequestException("System error, please try later");
        }
    }

    public async Task<ResponseUser?> GetCurrentUserDto()
    {
        try
        {
            var token = _jwtService.GetCurrentToken();
            var result = await GetUserByToken(token!);
            if (result is null)
            {
                throw new BadRequestException("User not found");
            }
            return _mapper.Map<ResponseUser>(result);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in {ClassName}, at {MethodName}: \nError: {Error} \nDetails: {Details}", nameof(UserServiceRepository), nameof(GetCurrentUserDto), ex.Message, ex.InnerException?.Message);
            throw new BadRequestException("System error, please try later");
        }
    }

    public async Task<User?> GetUserById(Guid id)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user is null)
            {
                throw new BadRequestException("User not found");
            }
            return user;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in {ClassName}, at {MethodName}: \nError: {Error} \nDetails: {Details}", nameof(UserServiceRepository), nameof(GetUserById), ex.Message, ex.InnerException?.Message);
            throw new BadRequestException("User not found");
        }
    }

    public async Task<User?> GetUserByToken(string token)
    {
        try
        {
            TokenValidationParameters validationParameters = _jwtService.GetJwtParams();
            Claim? claim = _jwtService.GetClaim(token, ClaimTypes.Email);
            if (claim is null)
            {
                throw new BadRequestException("User not found");
            }
            string email = claim.Value;
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
            {
                throw new BadRequestException("User not found");
            }
            return user;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in {ClassName}, at {MethodName}: \nError: {Error} \nDetails: {Details}", nameof(UserServiceRepository), nameof(GetUserByToken), ex.Message, ex.InnerException?.Message);
            throw new BadRequestException("System error, lease try later");
        }
    }

    public async Task<ResponseUser?> GetUserDtoById(Guid id)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(id.ToString()) ?? throw new BadRequestException("User not found");
            var userDto = _mapper.Map<ResponseUser>(user);
            return userDto;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in {ClassName}, at {MethodName}: \nError: {Error} \nDetails: {Details}", nameof(UserServiceRepository), nameof(GetUserDtoById), ex.Message, ex.InnerException?.Message);
            throw new BadRequestException("System error, please try later");
        }
    }

    public async Task<ResponseUser?> GetUserDtoByToken(string token)
    {
        try
        {
            TokenValidationParameters validationParameters = _jwtService.GetJwtParams();
            Claim? claim = _jwtService.GetClaim(token, ClaimTypes.Email);
            if (claim is null)
            {
                throw new BadRequestException("User not found");
            }
            string email = claim.Value;
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
            {
                throw new BadRequestException("User not found");
            }
            var userDto = _mapper.Map<ResponseUser>(user);
            return userDto;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in {ClassName}, at {MethodName}: \nError: {Error} \nDetails: {Details}", nameof(UserServiceRepository), nameof(GetUserDtoByToken), ex.Message, ex.InnerException?.Message);
            throw new BadRequestException("Get user by token failed, please try later");
        }
    }

    public async Task<ResponseCommand> RemoveUser(Guid id)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(id.ToString()) ?? throw new BadRequestException("User not found");
            var check = await _userManager.DeleteAsync(user);
            if (check.Succeeded != true)
            {
                _logger.LogError("Error in {ClassName}, at {MethodName}: \nError: {Error}", nameof(UserServiceRepository), nameof(RemoveUser), check.Errors.ToString());
                throw new BadRequestException("Remove user failed");
            }
            var result = _mapper.Map<ResponseCommand>(user);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in {ClassName}, at {MethodName}: \nError: {Error} \nDetails: {Details}", nameof(UserServiceRepository), nameof(RemoveUser), ex.Message, ex.InnerException?.Message);
            throw new BadRequestException("System error, please try later");
        }
    }

    public async Task<ResponseCommand> UpdateUser(CommandUser commandUser)
    {
        try
        {
            var token = _jwtService.GetCurrentToken();
            var user = await GetUserByToken(token!);
            if (user is null)
            {
                throw new BadRequestException("User not found");
            }
            _mapper.Map(commandUser, user);
            var check = await _userManager.UpdateAsync(user);
            if (check.Succeeded != true)
            {
                _logger.LogError("Error in {ClassName}, at {MethodName}: \nError: {Error}", nameof(UserServiceRepository), nameof(UpdateUser), check.Errors.ToString());
                throw new BadRequestException("Update user failed");
            }
            var result = _mapper.Map<ResponseCommand>(user);
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new BadRequestException("System error, please try later");
        }
    }
}