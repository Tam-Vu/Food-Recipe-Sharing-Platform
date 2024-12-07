using FoodRecipeSharingPlatform.Enitities;
using FoodRecipeSharingPlatform.Repositories;

namespace FoodRecipeSharingPlatform.Interfaces;

public interface IRepositoryFactory
{
    IBaseRepository<TEntity, Guid> GetRepository<TEntity, Guid>() where TEntity : BaseEntity;
}