using AutoMapper;
using FoodRecipeSharingPlatform.Enitities;
using FoodRecipeSharingPlatform.Entities.Models;

namespace FoodRecipeSharingPlatform.Dtos.FoodDto.CommandFood.CommandFood;

public class CommandFoodMapping : Profile
{
    public CommandFoodMapping()
    {
        CreateMap<CommandFood, Food>();

        CreateMap<CommandFood, Food>()
            .ForMember(dest => dest.FoodIngredients, opt => opt.MapFrom(src => src.CommandFoodIngredient))
            .ForMember(dest => dest.Steps, opt => opt.MapFrom(src => src.CommandStep));

        CreateMap<Food, ResponseCommand>();
    }
}