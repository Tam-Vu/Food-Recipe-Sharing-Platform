using AutoMapper;
using FoodRecipeSharingPlatform.Enitities;
using FoodRecipeSharingPlatform.Entities.Models;

namespace FoodRecipeSharingPlatform.Dtos.Gredient.CommandIngredient;

public class CommandGredientMapping : Profile
{
    public CommandGredientMapping()
    {
        CreateMap<CommandIngredient, Ingredient>();
        CreateMap<Ingredient, ResponseCommand>();
    }
}