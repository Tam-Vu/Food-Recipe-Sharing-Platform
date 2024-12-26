
using FoodRecipeSharingPlatform.Dtos.IngredientDto.CommandIngredient;
using FoodRecipeSharingPlatform.Dtos.IngredientDto.ResposeIngredient;
using FoodRecipeSharingPlatform.Enitities;
using FoodRecipeSharingPlatform.Entities.Models;

namespace FoodRecipeSharingPlatform.Interfaces;
public interface IIngredientRepository : IBaseRepository<Ingredient, Guid, CommandIngredient>
{
    Task<List<ResposeIngredient>> GetAllIngredients(CancellationToken cancellationToken);
    Task<ResponseCommand> AddIngredient(CommandIngredient commandIngredient, CancellationToken cancellationToken);
    Task<ResponseCommand> UpdateIngredient(Guid id, CommandIngredient commandIngredient, CancellationToken cancellationToken);
    Task<List<ResposeIngredient>> GetAllIngredientsByName(string name, CancellationToken cancellationToken);

}