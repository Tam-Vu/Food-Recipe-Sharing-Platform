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
using Microsoft.IdentityModel.Tokens;

namespace FoodRecipeSharingPlatform.Repositories;

public class UserServiceRepository : IUserServiceRepository
{
    private readonly IBaseRepository<User, Guid, ResponseCommand> _userRepository;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    private readonly IJwtService _jwtService;
    public UserServiceRepository(IRepositoryFactory repositoryFactory, UserManager<User> userManager, IMapper mapper, IJwtService jwtService)
    {
        _userRepository = repositoryFactory.GetRepository<User, Guid, ResponseCommand>();
        _userManager = userManager;
        _mapper = mapper;
        _jwtService = jwtService;
    }

    public Task<List<ResponseUser>> GetAllUser()
    {
        try
        {
            throw new NotImplementedException();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
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
        catch (Exception e)
        {
            Console.WriteLine(e);
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
        catch (Exception e)
        {
            Console.WriteLine(e);
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
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new BadRequestException("System error, lease try later");
        }
    }

    public Task<ResponseUser?> GetUserDtoById(Guid id)
    {
        throw new NotImplementedException();
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
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new BadRequestException("Get user by token failed, please try later");
        }
    }

    public Task<ResponseCommand> RemoveUser(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<ResponseCommand> UpdateUser(CommandUser commandUser)
    {
        try
        {
            var user = _mapper.Map<User>(commandUser);
            var check = await _userManager.UpdateAsync(user);
            if (check.Succeeded != true)
            {
                //log error here
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