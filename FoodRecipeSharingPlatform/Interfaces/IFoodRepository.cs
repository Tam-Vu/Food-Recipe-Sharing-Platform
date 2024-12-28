using FoodRecipeSharingPlatform.Dtos.FoodDto.CommandFood.CommandFood;
using FoodRecipeSharingPlatform.Enitities;
using FoodRecipeSharingPlatform.Entities.Models;

namespace FoodRecipeSharingPlatform.Interfaces;

public interface IFoodRepository : IBaseRepository<Food, Guid, CommandFood>
{
    Task<ResponseCommand> CreateFoodAsync(CommandFood commandFoodDto, CancellationToken cancellationToken);
    // Task UpdateFoodAsync(Food food);
    // Task DeleteFoodAsync(Food food);
    // Task<IEnumerable<Food>> GetFoodsAsync();
    // Task<Food> GetFoodByIdAsync(Guid id);

}
