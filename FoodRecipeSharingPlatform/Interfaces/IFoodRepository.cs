using Azure;
using FoodRecipeSharingPlatform.Dtos.FoodDto.CommandFood.CommandFood;
using FoodRecipeSharingPlatform.Dtos.FoodDto.ResponseFood.ResponseFood;
using FoodRecipeSharingPlatform.Enitities;
using FoodRecipeSharingPlatform.Entities.Models;

namespace FoodRecipeSharingPlatform.Interfaces;

public interface IFoodRepository : IBaseRepository<Food, Guid, CommandFood>
{
    Task<ResponseCommand> CreateFoodAsync(CommandFood commandFoodDto, CancellationToken cancellationToken);
    Task<ResponseCommand> UpdateFoodAsync(Guid id, CommandFood commandFoodDto, CancellationToken cancellationToken);
    Task<ResponseCommand> DeleteFoodAsync(Guid id, CancellationToken cancellationToken);
    Task<List<ResponseListFood>> GetFoodsByNameAsync(string name, CancellationToken cancellationToken);
    Task<List<ResponseListFood>> GetFoodsByCategory(Guid CategoryId, CancellationToken cancellationToken);
    Task<List<ResponseListFood>> GetFoodsOfCurrentUser(CancellationToken cancellationToken);
    Task<List<ResponseListFood>> GetFoodsByUserId(Guid UserId, CancellationToken cancellationToken);
    Task<ResponseFood> GetFoodByIdAsync(Guid id, CancellationToken cancellationToken);
    // Task UpdateFoodAsync(Food food);
    // Task DeleteFoodAsync(Food food);
    // Task<IEnumerable<Food>> GetFoodsAsync();
    // Task<Food> GetFoodByIdAsync(Guid id);

}
