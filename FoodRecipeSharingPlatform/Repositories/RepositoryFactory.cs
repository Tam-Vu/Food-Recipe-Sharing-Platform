using FoodRecipeSharingPlatform.Enitities;
using FoodRecipeSharingPlatform.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace FoodRecipeSharingPlatform.Repositories;

public class RepositoryFactory : IRepositoryFactory
{
    private readonly IServiceProvider _serviceProvider;
    public RepositoryFactory(IServiceProvider serviceProvider)
    {
        this._serviceProvider = serviceProvider;
    }
    public IBaseRepository<TEntity, Guid> GetRepository<TEntity, Guid>() where TEntity : class
    {
        return this._serviceProvider.GetRequiredService<IBaseRepository<TEntity, Guid>>();
    }
}