using AutoMapper;
using FoodRecipeSharingPlatform.Enitities;

namespace FoodRecipeSharingPlatform.Dtos.FoodDto.CommandFood.CommandFoodIngredient;

public class CommandFoodIngredientMapping : Profile
{
    public CommandFoodIngredientMapping()
    {
        CreateMap<CommandFoodIngredients, FoodIngredient>();
    }
}