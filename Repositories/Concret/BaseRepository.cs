using Company_Expense_Tracker.DataAccess;
using Company_Expense_Tracker.Entities;
using Microsoft.EntityFrameworkCore;

namespace Company_Expense_Tracker.Repositories.Concret;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    private readonly DataContext _dataContext;
    private readonly DbSet<TEntity> _dbSet;

    protected BaseRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
        _dbSet = dataContext.Set<TEntity>();
    }
    
    public async Task<List<TEntity>> GetAllAsync()
    {
        return await _dbSet.Where(t => !t.IsDeleted).ToListAsync();
    }

    public async Task<TEntity> GetByIdAsync(int id)
    {
        return await _dbSet.Where(t => !t.IsDeleted && t.Id == id).FirstOrDefaultAsync() 
               ?? throw new NullReferenceException($"No {typeof(TEntity).Name} found with Id {id} in the database.");
    }

    public async Task CreateAsync(TEntity entity)
    {
        _dbSet.Add(entity);
        await _dataContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        _dbSet.Update(entity);
        await _dataContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(TEntity entity)
    {
        entity.IsDeleted = true;
        await _dataContext.SaveChangesAsync();
    }
}