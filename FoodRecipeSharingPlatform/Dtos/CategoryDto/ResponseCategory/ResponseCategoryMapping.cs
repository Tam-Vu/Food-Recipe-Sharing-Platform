using AutoMapper;
using FoodRecipeSharingPlatform.Enitities;

namespace FoodRecipeSharingPlatform.Dtos.CategoryDto.ResponseCategory;

public class ResponseCategoryMapping : Profile
{
    public ResponseCategoryMapping()
    {
        CreateMap<Category, ResponseCategory>();
    }
}