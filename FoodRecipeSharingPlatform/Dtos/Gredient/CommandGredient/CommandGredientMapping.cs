using AutoMapper;
using FoodRecipeSharingPlatform.Enitities;

namespace FoodRecipeSharingPlatform.Dtos.Gredient.CommandGredient;

public class CommandGredientMapping : Profile
{
    public CommandGredientMapping()
    {
        CreateMap<CommandGredient, Ingredient>();
    }
}