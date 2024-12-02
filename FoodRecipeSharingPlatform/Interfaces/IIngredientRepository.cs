using FoodRecipeSharingPlatform.Dtos.Gredient.CommandGredient;
using FoodRecipeSharingPlatform.Enitities;

namespace FoodRecipeSharingPlatform.Interfaces
{
    public interface IIngredientRepository : IBaseRepository<Ingredient, Guid, CommandGredient>
    {

    }
}