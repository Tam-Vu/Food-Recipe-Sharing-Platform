using System.Linq.Expressions;
using AutoMapper;
using EFCore.BulkExtensions;
using FoodRecipeSharingPlatform.Data.Common;
using FoodRecipeSharingPlatform.Enitities.Models;
using FoodRecipeSharingPlatform.Entities.Models;
using FoodRecipeSharingPlatform.Exceptions;
using FoodRecipeSharingPlatform.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace FoodRecipeSharingPlatform.Repositories;
public class BaseRepository<TEntity, TKey, TDto> : IBaseRepository<TEntity, TKey, TDto>
       where TEntity : class where TDto : class
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
        var entity = _mapper.Map<TEntity>(dto);
        var result = (await _dbSet.AddAsync(entity, cancellationToken)).Entity;
        await _context.SaveChangesAsync(cancellationToken);
        var final = _mapper.Map<ResponseCommand>(result);
        return final;
    }

    public virtual async Task<ResponseCommand> AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        var result = (await _dbSet.AddAsync(entity, cancellationToken)).Entity;
        await _context.SaveChangesAsync(cancellationToken);
        var final = _mapper.Map<ResponseCommand>(result);
        return final;
    }

    public virtual async Task<IEnumerable<ResponseCommand>> AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
    {
        await _dbSet.AddRangeAsync(entities);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<IEnumerable<ResponseCommand>>(entities);
    }

    public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
    {
        var entities = await _dbSet.AnyAsync(predicate, cancellationToken);
        return entities;
    }

    public virtual async Task<ResponseCommand> DeleteAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await _context.BulkDeleteAsync(new List<TEntity> { entity });
        var result = _mapper.Map<ResponseCommand>(entity);
        return result;
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

    public virtual async Task<ResponseCommand> DeleteByIdAsync(TKey id, CancellationToken cancellationToken)
    {
        var entity = await _dbSet.FindAsync(id);
        // if (entity != null)
        // {
        // }
        _dbSet.RemoveRange(entity!);
        await _context.SaveChangesAsync(cancellationToken);
        _mapper.Map<ResponseCommand>(entity);
        return _mapper.Map<ResponseCommand>(entity);
    }

    public virtual async Task DeleteByModel(TEntity entity, CancellationToken cancellationToken)
    {
        await _context.BulkDeleteAsync(new List<TEntity> { entity });
    }

    public virtual async Task DeleteByMultiModel(List<TEntity> entities, CancellationToken cancellationToken)
    {
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
    public virtual async Task DeleteByMulti(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
    {
        var entity = await _dbSet.Where(predicate).ToListAsync();
        if (entity != null)
        {
            _dbSet.RemoveRange(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public virtual async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken)
    {
        var entities = await _dbSet.Where(predicate).ToListAsync(cancellationToken);
        return entities;
    }

    public virtual async Task<List<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeQuery,
    CancellationToken cancellationToken)
    {
        var query = includeQuery(_dbSet);
        var entities = await query.ToListAsync(cancellationToken);
        return entities;
    }

    public virtual async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeQuery,
        CancellationToken cancellationToken)
    {
        var query = _dbSet.Where(predicate);
        query = includeQuery(query);
        var entities = await query.ToListAsync(cancellationToken);
        return entities;
    }

    public virtual async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> sort,
        CancellationToken cancellationToken)
    {
        var query = _dbSet.Where(predicate);
        query = sort(query);
        var entities = await query.ToListAsync(cancellationToken);
        return entities;
    }

    public virtual async Task<PaginationResponse<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> sort,
        int pageIndex,
        int pageSize,
        CancellationToken cancellationToken)
    {
        var query = sort(_dbSet.Where(predicate));
        return await this.GetPaginationEntities(query, pageIndex, pageSize, cancellationToken);
    }

    public virtual async Task<PaginationResponse<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate,
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

    public virtual async Task<TEntity> GetByIdAsync(TKey id, CancellationToken cancellationToken)
    {
        var entity = await _dbSet.FindAsync(id, cancellationToken);
        return entity ?? throw new BadRequestException($"{nameof(TEntity)} not found");
    }

    public virtual async Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken)
    {
        var query = _dbSet.Where(predicate);
        var entity = await query.FirstAsync(cancellationToken);
        return entity ?? throw new BadRequestException($"{nameof(TEntity)} not found");
    }

    public virtual async Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken)
    {
        var query = _dbSet.Where(predicate);
        var entity = await query.FirstOrDefaultAsync(cancellationToken);
        return entity ?? null;
    }

    public virtual async Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeQuery,
        CancellationToken cancellationToken)
    {
        var query = _dbSet.Where(predicate);
        query = includeQuery(query);
        var entity = await query.FirstAsync(cancellationToken);
        return entity ?? throw new BadRequestException($"{nameof(TEntity)} not found");
    }
    public IQueryable<TEntity> All()
    {
        return _dbSet;
    }

    public virtual async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken)
    {
        var query = _dbSet.Where(predicate);
        var entity = await query.SingleAsync(cancellationToken);
        return entity ?? throw new BadRequestException($"{nameof(TEntity)} not found");
    }

    public virtual async Task<TEntity?> GetSingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate,
    CancellationToken cancellationToken)
    {
        var query = _dbSet.Where(predicate);
        var entity = await query.SingleAsync(cancellationToken);
        return entity ?? null;
    }

    public virtual async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeQuery,
        CancellationToken cancellationToken)
    {
        var query = _dbSet.Where(predicate);
        query = includeQuery(query);
        var entity = await query.SingleAsync(cancellationToken);
        return entity ?? throw new BadRequestException($"{nameof(TEntity)} not found");
    }

    public virtual async Task<ResponseCommand> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<ResponseCommand>(entity);
    }

    public virtual async Task<ResponseCommand> UpdateAsync(Guid id, TDto dto, CancellationToken cancellationToken)
    {
        var entity = await _dbSet.FindAsync(id);
        _mapper.Map(dto, entity);
        _dbSet.Update(entity!);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<ResponseCommand>(entity);
    }

    public virtual async Task<List<ResponseCommand>> UpdateRangeAsync(List<TEntity> entities, CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<List<ResponseCommand>>(entities);
    }

    protected async Task<PaginationResponse<TEntity>> GetPaginationEntities(IQueryable<TEntity> query, int pageIndex, int pageSize, CancellationToken cancellationToken)
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
        };
        return response;
    }

    public virtual async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        var entities = await _dbSet.ToListAsync();
        return entities;
    }

    public async Task<TEntity?> FindOneAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
    {
        var query = _dbSet.Where(predicate);
        var entity = await query.FirstOrDefaultAsync(cancellationToken);
        return entity;
    }
}