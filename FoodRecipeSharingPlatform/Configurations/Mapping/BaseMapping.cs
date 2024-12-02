using AutoMapper;
using FoodRecipeSharingPlatform.Dtos.Gredient.CommandGredient;
using FoodRecipeSharingPlatform.Enitities;
using FoodRecipeSharingPlatform.Entities.Models;

namespace FoodRecipeSharingPlatform.Configurations.Mapping;

public class BaseMapping : Profile
{
    public BaseMapping()
    {
        CreateMap<Ingredient, ResponseCommand>();
        CreateMap<Ingredient, CommandGredient>();
    }
}