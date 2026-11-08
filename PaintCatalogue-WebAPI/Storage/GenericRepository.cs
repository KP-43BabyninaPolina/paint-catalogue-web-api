using Microsoft.EntityFrameworkCore;
using PaintCatalogue_WebAPI.DataModel;
using PaintCatalogue_WebAPI.Storage.Contracts;

namespace PaintCatalogue_WebAPI.Storage;

internal class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : class
{
    protected readonly DataModelContext _context;

    protected GenericRepository(DataModelContext context) => _context = context;

    public virtual IQueryable<TEntity> GetAll() => _context.Set<TEntity>();

    public virtual Task<TEntity?> FindAsync(TKey key) => _context.Set<TEntity>().FindAsync(key).AsTask();

    public virtual async Task AddAsync(TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task UpdateAsync(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
        await _context.SaveChangesAsync();
    }
    
    public virtual async Task DeleteAllAsync()
    {
        await _context.Set<TEntity>().ExecuteDeleteAsync();
    }
}