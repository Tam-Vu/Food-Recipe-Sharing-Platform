using AutoMapper;
using FoodRecipeSharingPlatform.Enitities;

namespace FoodRecipeSharingPlatform.Dtos.FoodDto.ResponseFood.ResponseFoodIngredient;
public class ResponseFoodIngredientMapping : Profile
{
    public ResponseFoodIngredientMapping()
    {
        CreateMap<FoodIngredient, ResponseFoodIngredients>();
    }
}