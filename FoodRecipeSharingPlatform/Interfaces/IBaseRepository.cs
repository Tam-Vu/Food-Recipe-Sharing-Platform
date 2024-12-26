using System.Linq.Expressions;
using FoodRecipeSharingPlatform.Enitities;
using FoodRecipeSharingPlatform.Enitities.Models;
using FoodRecipeSharingPlatform.Entities.Models;
using Microsoft.EntityFrameworkCore.Query;

namespace FoodRecipeSharingPlatform.Interfaces;


public interface IBaseRepository<TEntity, TKey, TDto> where TEntity : class where TDto : class
{
    IQueryable<TEntity> All();

    Task<TEntity> GetByIdAsync(TKey id, CancellationToken cancellationToken);

    Task<TEntity?> FindOneAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken);

    Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken);

    Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeQuery,
        CancellationToken cancellationToken);

    Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken);

    Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeQuery,
        CancellationToken cancellationToken);

    Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken);

    Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken);


    Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeQuery,
        CancellationToken cancellationToken);

    Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> sort,
        CancellationToken cancellationToken);

    Task<PaginationResponse<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> sort,
        int pageIndex,
        int pageSize,
        CancellationToken cancellationToken);

    Task<PaginationResponse<TEntity>> GetAllAsync(
       Expression<Func<TEntity, bool>> predicate,
       Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> sort,
       int pageIndex,
       int pageSize,
       Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeQuery,
       CancellationToken cancellationToken);

    Task<ResponseCommand> AddAsync(TDto dto, CancellationToken cancellationToken);

    Task<IEnumerable<ResponseCommand>> AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);

    Task<ResponseCommand> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
    Task<ResponseCommand> UpdateAsync(Guid id, TDto dto, CancellationToken cancellationToken);

    Task<List<ResponseCommand>> UpdateRangeAsync(List<TEntity> entities, CancellationToken cancellationToken);

    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

    Task<ResponseCommand> DeleteAsync(TEntity entity, CancellationToken cancellationToken);

    Task<ResponseCommand> DeleteByIdAsync(TKey id, CancellationToken cancellationToken);
    Task DeleteMultiByIdAsync(ICollection<TKey> ids, CancellationToken cancellationToken);
    Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
    Task DeleteByModel(TEntity entity, CancellationToken cancellationToken);
    Task DeleteByMultiModel(List<TEntity> entities, CancellationToken cancellationToken);
}
