using FoodRecipeSharingPlatform.Dtos.CategoryDto.CommandCategory;
using FoodRecipeSharingPlatform.Dtos.CategoryDto.ResponseCategory;
using FoodRecipeSharingPlatform.Enitities;
using FoodRecipeSharingPlatform.Entities.Models;

namespace FoodRecipeSharingPlatform.Interfaces;

public interface ICategoryRepository : IBaseRepository<Category, Guid, CommandCategory>
{
    Task<ResponseCommand> AddCategory(CommandCategory commandCategory, CancellationToken cancellationToken);
    Task<ResponseCommand> UpdateCategory(Guid id, CommandCategory commandCategory, CancellationToken cancellationToken);
    Task<List<ResponseCategory>> GetAllCategories(CancellationToken cancellationToken);
    Task<List<ResponseCategory>> GetAllCategoriesByName(string name, CancellationToken cancellationToken);

}