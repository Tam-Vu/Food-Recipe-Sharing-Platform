using System.Globalization;
using System.Linq.Expressions;
using AutoMapper;
using EFCore.BulkExtensions;
using FoodRecipeSharingPlatform.Data.Common;
using FoodRecipeSharingPlatform.Enitities.Models;
using FoodRecipeSharingPlatform.Entities.Models;
using FoodRecipeSharingPlatform.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace FoodRecipeSharingPlatform.Repositories;
public class BaseRepository<TEntity, TKey, TDto> : IBaseRepository<TEntity, TKey, TDto>
       where TEntity : class
       where TDto : class
{
    protected readonly ApplicationDbContext _context;
    protected readonly IMapper _mapper;
    protected readonly DbSet<TEntity> _dbSet;
    public BaseRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        _dbSet = _context.Set<TEntity>();
    }
    public virtual async Task<ResponseCommand> AddAsync(TDto dto, CancellationToken cancellationToken)
    {
        var Entity = _mapper.Map<TEntity>(dto);
        var result = (await _dbSet.AddAsync(Entity, cancellationToken)).Entity;
        await _context.SaveChangesAsync(cancellationToken);
        var test = _mapper.Map<ResponseCommand>(result);
        return test;
    }

    public virtual async Task<IEnumerable<ResponseCommand>> AddRangeAsync(IEnumerable<TDto> dtos, CancellationToken cancellationToken)
    {
        var entities = _mapper.Map<IEnumerable<TEntity>>(dtos).ToList();
        await _dbSet.AddRangeAsync(entities);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<IEnumerable<ResponseCommand>>(entities);
    }

    public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
    {
        var entities = await _dbSet.AnyAsync(predicate, cancellationToken);
        return entities;
    }

    public virtual async Task DeleteAsync(TDto dto, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<TEntity>(dto);
        await _context.BulkDeleteAsync(new List<TEntity> { entity });
    }

    public virtual async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
    {
        var entity = await _dbSet.Where(predicate).ToListAsync();
        if (entity != null)
        {
            _dbSet.RemoveRange(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public virtual async Task DeleteByIdAsync(TKey id, CancellationToken cancellationToken)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity != null)
        {
            _dbSet.RemoveRange(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public virtual async Task DeleteByModel(TDto dto, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<TEntity>(dto);
        await _context.BulkDeleteAsync(new List<TEntity> { entity });
    }

    public virtual async Task DeleteByMultiModel(List<TDto> dtos, CancellationToken cancellationToken)
    {
        var entities = _mapper.ProjectTo<TEntity>(dtos.AsQueryable()).ToList();
        await _context.BulkDeleteAsync(entities);
    }

    public virtual async Task DeleteMultiByIdAsync(ICollection<TKey> ids, CancellationToken cancellationToken)
    {
        var entity = await _dbSet.FindAsync(ids);
        if (entity != null)
        {
            _dbSet.RemoveRange(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public virtual async Task<List<TDto>> GetAllAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken)
    {
        var entities = await _dbSet.Where(predicate).ToListAsync(cancellationToken);
        return _mapper.Map<List<TDto>>(entities);
    }

    public virtual async Task<List<TDto>> GetAllAsync(Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeQuery,
        CancellationToken cancellationToken)
    {
        var query = _dbSet.Where(predicate);
        query = includeQuery(query);
        var entities = await query.ToListAsync(cancellationToken);
        return _mapper.Map<List<TDto>>(entities);
    }

    public virtual async Task<List<TDto>> GetAllAsync(Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> sort,
        CancellationToken cancellationToken)
    {
        var query = _dbSet.Where(predicate);
        query = sort(query);
        var entities = await query.ToListAsync(cancellationToken);
        return _mapper.Map<List<TDto>>(entities);
    }

    public virtual async Task<PaginationResponse<TDto>> GetAllAsync(Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> sort,
        int pageIndex,
        int pageSize,
        CancellationToken cancellationToken)
    {
        var query = sort(_dbSet.Where(predicate));
        return await this.GetPaginationEntities(query, pageIndex, pageSize, cancellationToken);
    }

    public virtual async Task<PaginationResponse<TDto>> GetAllAsync(Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> sort,
        int pageIndex,
        int pageSize,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeQuery,
        CancellationToken cancellationToken)
    {
        var query = _dbSet.Where(predicate);
        query = includeQuery(query);
        query = sort(query);
        return await this.GetPaginationEntities(query, pageIndex, pageSize, cancellationToken);
    }

    public virtual async Task<TDto> GetByIdAsync(TKey id, CancellationToken cancellationToken)
    {
        var entity = await _dbSet.FindAsync(id, cancellationToken);
        return _mapper.Map<TDto>(entity);
    }

    public virtual async Task<TDto> GetFirstAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken)
    {
        var query = _dbSet.Where(predicate);
        var entity = await query.FirstAsync(cancellationToken);
        return _mapper.Map<TDto>(entity);
    }

    public virtual async Task<TDto> GetFirstAsync(Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeQuery,
        CancellationToken cancellationToken)
    {
        var query = _dbSet.Where(predicate);
        query = includeQuery(query);
        var entity = await query.FirstAsync(cancellationToken);
        return _mapper.Map<TDto>(entity);
    }
    public IQueryable<TEntity> All()
    {
        return _dbSet;
    }

    public virtual async Task<TDto> GetSingleAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken)
    {
        var query = _dbSet.Where(predicate);
        var entity = await query.SingleAsync(cancellationToken);
        return _mapper.Map<TDto>(entity);
    }

    public virtual async Task<TDto> GetSingleAsync(Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeQuery,
        CancellationToken cancellationToken)
    {
        var query = _dbSet.Where(predicate);
        query = includeQuery(query);
        var entity = await query.SingleAsync(cancellationToken);
        return _mapper.Map<TDto>(entity);
    }

    public virtual async Task<ResponseCommand> UpdateAsync(TDto dto, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<TEntity>(dto);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<ResponseCommand>(entity);
    }

    public virtual async Task<List<ResponseCommand>> UpdateRangeAsync(List<TDto> dtos, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<List<TEntity>>(dtos).ToList();
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<List<ResponseCommand>>(entity);
    }

    protected async Task<PaginationResponse<TDto>> GetPaginationEntities(IQueryable<TEntity> query, int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        var totalItems = await query.CountAsync();
        var response = new PaginationResponse<TEntity>
        {
            PageIndex = pageIndex,
            PageSize = pageSize,
            TotalCount = totalItems,
            Items = Enumerable.Empty<TEntity>()
        };
        if (totalItems > 0)
        {
            response.Items = await query.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync(cancellationToken);
        }
        var result = _mapper.Map<PaginationResponse<TDto>>(response);
        return result;
    }

    public virtual async Task<List<TDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var entities = await _dbSet.ToListAsync();
        return _mapper.Map<List<TDto>>(entities);
    }
}