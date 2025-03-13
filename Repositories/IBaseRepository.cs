namespace Company_Expense_Tracker.Repositories;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task<List<TEntity>> GetAllAsync();
    Task<TEntity> GetByIdAsync(int id);
    Task CreateAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
}