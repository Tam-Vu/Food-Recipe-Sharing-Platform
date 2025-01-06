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

        CreateMap<Food, Food>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        // .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
        // .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
        // .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}