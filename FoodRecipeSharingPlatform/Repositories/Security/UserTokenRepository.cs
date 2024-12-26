using AutoMapper;
using FoodRecipeSharingPlatform.Data.Common;
using FoodRecipeSharingPlatform.Data.Migrations;
using FoodRecipeSharingPlatform.Enitities.Identity;
using FoodRecipeSharingPlatform.Interfaces;
using FoodRecipeSharingPlatform.Interfaces.Security;
using Microsoft.AspNetCore.Identity;

namespace FoodRecipeSharingPlatform.Repositories.Security;

public class UserTokenRepository : BaseRepository<IdentityUserToken<Guid>, IdentityUserToken<Guid>, UserToken>, IUserTokenRepository
{
    private readonly IBaseRepository<IdentityUserToken<Guid>, IdentityUserToken<Guid>, UserToken> _userTokenRepository;

    public UserTokenRepository(ApplicationDbContext context, IMapper mapper, IRepositoryFactory repositoryFactory) : base(context, mapper)
    {
        _userTokenRepository = repositoryFactory.GetRepository<IdentityUserToken<Guid>, IdentityUserToken<Guid>, UserToken>();
    }
}