using AutoMapper;
using FoodRecipeSharingPlatform.Data.Common;
using FoodRecipeSharingPlatform.Dtos.Gredient.CommandGredient;
using FoodRecipeSharingPlatform.Enitities;
using FoodRecipeSharingPlatform.Repositories;

namespace FoodRecipeSharingPlatform.Interfaces;

public class IngredientRepository : BaseRepository<Ingredient, Guid, CommandGredient>, IIngredientRepository
{
    public IngredientRepository(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
    }
}