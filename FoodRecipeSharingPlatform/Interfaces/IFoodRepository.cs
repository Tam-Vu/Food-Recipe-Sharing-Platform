using Azure;
using FoodRecipeSharingPlatform.Dtos.FoodDto.CommandFood.CommandFood;
using FoodRecipeSharingPlatform.Dtos.FoodDto.ResponseFood.ResponseFood;
using FoodRecipeSharingPlatform.Enitities;
using FoodRecipeSharingPlatform.Entities.Models;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace FoodRecipeSharingPlatform.Interfaces;

public interface IFoodRepository : IBaseRepository<Food, Guid, CommandFood>
{
    Task<ResponseCommand> CreateFoodAsync(CommandFood commandFoodDto, CancellationToken cancellationToken);
    // Task<List<ResponseFood>> GetFoodDetails(Guid id, CancellationToken cancellationToken);
    Task<List<ResponseListFood>> GetFoodsByNameAsync(string name, CancellationToken cancellationToken);
    // Task UpdateFoodAsync(Food food);
    // Task DeleteFoodAsync(Food food);
    // Task<IEnumerable<Food>> GetFoodsAsync();
    // Task<Food> GetFoodByIdAsync(Guid id);

}
