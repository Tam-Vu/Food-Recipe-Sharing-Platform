using AutoMapper;
using FoodRecipeSharingPlatform.Enitities;

namespace FoodRecipeSharingPlatform.Dtos.FoodDto.ResponseFood.ResponseFoodIngredient;
public class ResponseFoodIngredientMapping : Profile
{
    public ResponseFoodIngredientMapping()
    {
        CreateMap<FoodIngredient, ResponseFoodIngredients>()
        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Ingredient.Name))
        .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));
    }
}