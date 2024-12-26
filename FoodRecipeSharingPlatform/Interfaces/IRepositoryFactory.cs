namespace FoodRecipeSharingPlatform.Interfaces;

public interface IRepositoryFactory
{
    IBaseRepository<TEntity, Guid, TDto> GetRepository<TEntity, Guid, TDto>() where TEntity : class where TDto : class;
}