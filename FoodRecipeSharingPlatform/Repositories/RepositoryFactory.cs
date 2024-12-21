using FoodRecipeSharingPlatform.Enitities;
using FoodRecipeSharingPlatform.Interfaces;

namespace FoodRecipeSharingPlatform.Repositories;

public class RepositoryFactory : IRepositoryFactory
{
    private readonly IServiceProvider _serviceProvider;
    public RepositoryFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    public IBaseRepository<TEntity, Guid> GetRepository<TEntity, Guid>() where TEntity : BaseEntity
    {
        return _serviceProvider.GetRequiredService<IBaseRepository<TEntity, Guid>>();
    }
}