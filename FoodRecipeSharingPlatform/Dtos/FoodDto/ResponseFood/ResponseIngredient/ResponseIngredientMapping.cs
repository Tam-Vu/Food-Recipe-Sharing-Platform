using AutoMapper;
using FoodRecipeSharingPlatform.Enitities;

namespace FoodRecipeSharingPlatform.Dtos.FoodDto.ResponseFood.ResponseIngredient;

public class ResponseIngredientMapping : Profile
{
    public ResponseIngredientMapping()
    {
        CreateMap<Ingredient, ResponseIngredients>();
    }
}