using FoodRecipeSharingPlatform.Enitities.Identity;
using Microsoft.AspNetCore.Identity;

namespace FoodRecipeSharingPlatform.Interfaces.Security;

public interface IUserTokenRepository : IBaseRepository<IdentityUserToken<Guid>, IdentityUserToken<Guid>, UserToken>
{

}