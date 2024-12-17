using AutoMapper;
using FoodRecipeSharingPlatform.Enitities.Identity;
using FoodRecipeSharingPlatform.Entities.Models;

namespace FoodRecipeSharingPlatform.Dtos.AuthDto.RegisterDto;

public class RegisterDtoMapping : Profile
{
    public RegisterDtoMapping()
    {
        CreateMap<RegisterDto, User>();
        CreateMap<User, RegisterDto>();
        CreateMap<User, ResponseCommand>();
    }
}