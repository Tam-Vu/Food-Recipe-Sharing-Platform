using AutoMapper;
using FoodRecipeSharingPlatform.Enitities;

namespace FoodRecipeSharingPlatform.Dtos.FoodDto.ResponseFood.ResponseFood;

public class ResponseFoodMapping : Profile
{
    public ResponseFoodMapping()
    {
        // CreateMap<Food, ResponseFood>()
        //     .ForMember("property", opt => opt.Ignore())
        //     .ForMember(
        //         dest => dest.Category,
        //         opt => opt.MapFrom(src => src.Category.Name)
        //     )
        //     .ForMember(
        //         dest => dest.UserId,
        //         opt => opt.MapFrom(src => src.UserId)
        //     )
        //     .ForMember(
        //         dest => dest.UserName,
        //         opt => opt.MapFrom(src => src.User.UserName)
        //     )
        //     .ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src =>
        //         src.FoodIngredients ?? null))
        //     .ForMember(dest => dest.Steps, opt => opt.MapFrom(src =>
        //         src.Steps ?? null));

        CreateMap<Food, ResponseListFood>();
    }
}