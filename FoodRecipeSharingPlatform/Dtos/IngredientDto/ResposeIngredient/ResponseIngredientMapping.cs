using AutoMapper;
using FoodRecipeSharingPlatform.Enitities;

namespace FoodRecipeSharingPlatform.Dtos.IngredientDto.ResposeIngredient;

public class ResponseGredientMapping : Profile
{
    public ResponseGredientMapping()
    {
        CreateMap<Ingredient, ResposeIngredient>();
    }
}