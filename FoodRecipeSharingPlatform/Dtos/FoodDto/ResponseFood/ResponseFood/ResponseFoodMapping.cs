using AutoMapper;
using FoodRecipeSharingPlatform.Enitities;

namespace FoodRecipeSharingPlatform.Dtos.FoodDto.ResponseFood.ResponseFood;

public class ResponseFoodMapping : Profile
{
    public ResponseFoodMapping()
    {
        CreateMap<Food, ResponseFood>();
        // .ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src =>
        //     src.FoodIngredients ?? null))
        // .ForMember(dest => dest.Steps, opt => opt.MapFrom(src =>
        //     src.Steps ?? null));

        CreateMap<Food, ResponseListFood>();
    }
}