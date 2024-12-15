using FoodRecipeSharingPlatform.Dtos.CategoryDto.CommandCategory;
using FoodRecipeSharingPlatform.Dtos.CategoryDto.ResponseCategory;
using FoodRecipeSharingPlatform.Entities.Models;

namespace FoodRecipeSharingPlatform.Interfaces;

public interface ICategoryRepository
{
    Task<ResponseCommand> AddCategory(CommandCategory commandCategory, CancellationToken cancellationToken);
    Task<ResponseCommand> UpdateCategory(Guid id, CommandCategory commandCategory, CancellationToken cancellationToken);
    Task<ResponseCommand> DeleteCategory(Guid id, CancellationToken cancellationToken);
    Task<List<ResponseCategory>> GetAllCategories(CancellationToken cancellationToken);
    // Task<List<ResponseCategory>> GetAllCategoriesByName(string name, CancellationToken cancellationToken);

}