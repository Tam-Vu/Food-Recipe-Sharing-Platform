using FoodRecipeSharingPlatform.Dtos.UserDto.CommnadUser;
using FoodRecipeSharingPlatform.Dtos.UserDto.ResponseUser;
using FoodRecipeSharingPlatform.Enitities.Identity;
using FoodRecipeSharingPlatform.Entities.Models;

namespace FoodRecipeSharingPlatform.Interfaces;

public interface IUserServiceRepository
{
    Task<ResponseUser?> GetUserDtoByToken(string token);
    Task<ResponseUser?> GetCurrentUserDto();
    Task<User?> GetUserByToken(string token);
    Task<ResponseUser?> GetUserDtoById(Guid id);
    Task<User?> GetUserById(Guid id);
    Task<ResponseCommand> RemoveUser(Guid id);
    Task<ResponseCommand> UpdateUser(CommandUser commandUser);
    Task<List<ResponseUser>> GetAllUser();

}