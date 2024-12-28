using Microsoft.EntityFrameworkCore;

namespace FoodRecipeSharingPlatform.Interfaces;

public interface IUnitOfWork : IDisposable
{
    void CreateTransaction();
    void Commit();
    void Rollback();
    Task SaveChangesAsync(CancellationToken cancellationToken);
}