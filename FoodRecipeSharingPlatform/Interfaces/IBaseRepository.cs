using System.Linq.Expressions;
using FoodRecipeSharingPlatform.Enitities;
using FoodRecipeSharingPlatform.Enitities.Models;
using FoodRecipeSharingPlatform.Entities.Models;
using Microsoft.EntityFrameworkCore.Query;

namespace FoodRecipeSharingPlatform.Interfaces;


public interface IBaseRepository<TEntity, TKey, TDto> where TEntity : class where TDto : class
{
    IQueryable<TEntity> All();

    Task<TDto> GetByIdAsync(TKey id, CancellationToken cancellationToken);

    Task<TDto> GetFirstAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken);

    Task<TDto> GetFirstAsync(Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeQuery,
        CancellationToken cancellationToken);

    Task<TDto> GetSingleAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken);

    Task<TDto> GetSingleAsync(Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeQuery,
        CancellationToken cancellationToken);

    Task<List<TDto>> GetAllAsync(CancellationToken cancellationToken);

    Task<List<TDto>> GetAllAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken);


    Task<List<TDto>> GetAllAsync(Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeQuery,
        CancellationToken cancellationToken);

    Task<List<TDto>> GetAllAsync(Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> sort,
        CancellationToken cancellationToken);

    Task<PaginationResponse<TDto>> GetAllAsync(Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> sort,
        int pageIndex,
        int pageSize,
        CancellationToken cancellationToken);

    Task<PaginationResponse<TDto>> GetAllAsync(
       Expression<Func<TEntity, bool>> predicate,
       Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> sort,
       int pageIndex,
       int pageSize,
       Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeQuery,
       CancellationToken cancellationToken);

    Task<ResponseCommand> AddAsync(TDto dto, CancellationToken cancellationToken);

    Task<IEnumerable<ResponseCommand>> AddRangeAsync(IEnumerable<TDto> dtos, CancellationToken cancellationToken);

    Task<ResponseCommand> UpdateAsync(TDto dto, CancellationToken cancellationToken);

    Task<List<ResponseCommand>> UpdateRangeAsync(List<TDto> dtos, CancellationToken cancellationToken);

    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

    Task DeleteAsync(TDto dto, CancellationToken cancellationToken);

    Task DeleteByIdAsync(TKey id, CancellationToken cancellationToken);
    Task DeleteMultiByIdAsync(ICollection<TKey> ids, CancellationToken cancellationToken);
    Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
    Task DeleteByModel(TDto dto, CancellationToken cancellationToken);
    Task DeleteByMultiModel(List<TDto> dtos, CancellationToken cancellationToken);
}
