using AutoMapper;
using FoodRecipeSharingPlatform.Enitities.Identity;

namespace FoodRecipeSharingPlatform.Dtos.UserDto.ResponseUser;

public class ResponseUserMapping : Profile
{
    public ResponseUserMapping()
    {
        CreateMap<User, ResponseUser>();
    }
}