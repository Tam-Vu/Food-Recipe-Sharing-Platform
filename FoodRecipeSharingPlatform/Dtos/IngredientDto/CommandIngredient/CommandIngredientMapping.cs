using AutoMapper;
using FoodRecipeSharingPlatform.Enitities;
using FoodRecipeSharingPlatform.Entities.Models;

namespace FoodRecipeSharingPlatform.Dtos.IngredientDto.CommandIngredient;

public class CommandGredientMapping : Profile
{
    public CommandGredientMapping()
    {
        CreateMap<CommandIngredient, Ingredient>();
        CreateMap<Ingredient, ResponseCommand>();
    }
}