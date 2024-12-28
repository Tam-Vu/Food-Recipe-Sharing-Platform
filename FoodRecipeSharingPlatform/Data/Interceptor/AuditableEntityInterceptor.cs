using FoodRecipeSharingPlatform.Enitities;
using FoodRecipeSharingPlatform.Enitities.Identity;
using FoodRecipeSharingPlatform.Entities.Identity;
using FoodRecipeSharingPlatform.Interfaces.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace FoodRecipeSharingPlatform.Data.Interceptor;

public class AuditableEntityInterceptor : SaveChangesInterceptor
{
    private readonly IIdentityService _identityService;
    private readonly TimeProvider _timeProvider;

    public AuditableEntityInterceptor(IIdentityService identityService, TimeProvider timeProvider)
    {
        _identityService = identityService;
        _timeProvider = timeProvider;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChanges(eventData, result);
    }
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateEntities(DbContext? context)
    {
        if (context == null) return;
        foreach (var entry in context.ChangeTracker.Entries<BaseEntity>())
        {
            if (entry.State is EntityState.Added or EntityState.Modified || entry.HasChangedOwnedEntities())
            {
                var utcNow = _timeProvider.GetUtcNow();
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = utcNow;
                    entry.Entity.CreatedBy = _identityService.GetUserId() ?? null;
                }
                entry.Entity.LastModifiedAt = utcNow;
                entry.Entity.LastModifiedBy = _identityService.GetUserId() ?? null;
            }
        }
        foreach (var entry in context.ChangeTracker.Entries())
        {
            if (entry.Entity is User || entry.Entity is RoleClaim || entry.Entity is UserRole
            || entry.Entity is UserClaim || entry.Entity is UserLogin || entry.Entity is UserToken)
            {
                if (entry.State is EntityState.Added or EntityState.Modified || entry.HasChangedOwnedEntities())
                {
                    var utcNow = _timeProvider.GetUtcNow();
                    if (entry.State == EntityState.Added)
                    {
                        (entry.Entity as dynamic).CreatedAt = utcNow;
                    }
                    (entry.Entity as dynamic).LastModifiedAt = utcNow;
                }
            }
        }
    }
}

public static class Extensions
{
    public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
        entry.References.Any(r =>
            r.TargetEntry != null &&
            r.TargetEntry.Metadata.IsOwned() &&
            (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified)
        );
}