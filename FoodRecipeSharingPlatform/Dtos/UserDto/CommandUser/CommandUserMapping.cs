using AutoMapper;
using FoodRecipeSharingPlatform.Enitities.Identity;

namespace FoodRecipeSharingPlatform.Dtos.UserDto.CommnadUser;

public class CommandUserMapping : Profile
{
    public CommandUserMapping()
    {
        CreateMap<CommandUser, User>();
        CreateMap<User, CommandUser>();
    }
}