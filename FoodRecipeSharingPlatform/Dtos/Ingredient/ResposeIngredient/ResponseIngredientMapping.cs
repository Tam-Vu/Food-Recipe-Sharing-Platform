using AutoMapper;
using FoodRecipeSharingPlatform.Enitities;

namespace FoodRecipeSharingPlatform.Dtos.Gredient.ResposeIngredient;

public class ResponseGredientMapping : Profile
{
    public ResponseGredientMapping()
    {
        CreateMap<Ingredient, ResposeIngredient>();
    }
}