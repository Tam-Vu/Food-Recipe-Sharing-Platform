using AutoMapper;
using FoodRecipeSharingPlatform.Entities.Models;
using FoodRecipeSharingPlatform.Enitities;

namespace FoodRecipeSharingPlatform.Dtos.CategoryDto.CommandCategory;

public class CommandCategoryMapping : Profile
{
    public CommandCategoryMapping()
    {
        CreateMap<Category, ResponseCommand>();
        CreateMap<CommandCategory, Category>();
    }
}