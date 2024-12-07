
using FoodRecipeSharingPlatform.Dtos.Gredient.CommandIngredient;
using FoodRecipeSharingPlatform.Dtos.Gredient.ResposeIngredient;
using FoodRecipeSharingPlatform.Entities.Models;

namespace FoodRecipeSharingPlatform.Interfaces;
public interface IIngredientRepository
{
    Task<List<ResposeIngredient>> GetAllIngredients(CancellationToken cancellationToken);
    Task<ResponseCommand> AddIngredient(CommandIngredient commandIngredient, CancellationToken cancellationToken);
    Task<ResponseCommand> UpdateIngredient(Guid id, CommandIngredient commandIngredient, CancellationToken cancellationToken);
    Task<ResponseCommand> DeleteIngredient(Guid id, CancellationToken cancellationToken);
}