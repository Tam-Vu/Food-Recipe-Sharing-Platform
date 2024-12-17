using FoodRecipeSharingPlatform.Enitities.Identity;
using FoodRecipeSharingPlatform.Interfaces;
using FoodRecipeSharingPlatform.Interfaces.Security;

namespace FoodRecipeSharingPlatform.Repositories.Security;

public class UserTokenRepository : IUserTokenRepository
{
    private readonly IBaseRepository<UserToken, Guid> _userTokenRepository;
    public UserTokenRepository(IRepositoryFactory repositoryFactory)
    {
        _userTokenRepository = repositoryFactory.GetRepository<UserToken, Guid>();
    }
}