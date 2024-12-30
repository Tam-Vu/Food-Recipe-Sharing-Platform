using AutoMapper;
using FoodRecipeSharingPlatform.Enitities;

namespace FoodRecipeSharingPlatform.Dtos.FoodDto.ResponseFood.ResponseStep;

public class ResponseStepMapping : Profile
{
    public ResponseStepMapping()
    {
        CreateMap<Step, ResponseSteps>();
    }
}