using AutoMapper;
using FoodRecipeSharingPlatform.Enitities;

namespace FoodRecipeSharingPlatform.Dtos.FoodDto.CommandFood.CommandStep;

public class CommandStepMapping : Profile
{
    public CommandStepMapping()
    {
        CreateMap<CommandSteps, Step>();
    }
}