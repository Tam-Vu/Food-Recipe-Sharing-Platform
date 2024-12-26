using FoodRecipeSharingPlatform.Data.Common;
using FoodRecipeSharingPlatform.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace FoodRecipeSharingPlatform.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;
    private IDbContextTransaction? _transaction;
    public UnitOfWork(ApplicationDbContext context)
    {
        _dbContext = context;
    }

    public void Commit()
    {
        _transaction?.Commit();
    }

    public void CreateTransaction()
    {
        _transaction = _dbContext.Database.BeginTransaction();
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        GC.SuppressFinalize(this);
    }

    public void Rollback()
    {
        _transaction?.Rollback();
    }
}