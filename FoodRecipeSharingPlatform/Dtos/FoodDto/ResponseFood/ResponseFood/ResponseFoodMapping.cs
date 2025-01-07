using AutoMapper;
using FoodRecipeSharingPlatform.Enitities;

namespace FoodRecipeSharingPlatform.Dtos.FoodDto.ResponseFood.ResponseFood;

public class ResponseFoodMapping : Profile
{
    public ResponseFoodMapping()
    {
        CreateMap<Food, ResponseFood>()
        .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
        .ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src => src.FoodIngredients))
        .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.User.FullName));

        CreateMap<Food, ResponseListFood>();
    }
}