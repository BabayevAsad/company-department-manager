using Company_Expense_Tracker.DataAccess;
using Company_Expense_Tracker.Entities;
using Microsoft.EntityFrameworkCore;

namespace Company_Expense_Tracker.Repositories.Concret;

public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
{
    private readonly DbSet<Department> _dbSet;
    private readonly DataContext _context; 

    public DepartmentRepository(DataContext dataContext) : base(dataContext)
    {
        _dbSet = dataContext.Set<Department>();
        _context = dataContext; 
    }

    public async Task<List<Department>> GetAllAsync()
    {
        return await _dbSet.Where(t => !t.IsDeleted).
            ToListAsync();
    }
    
    public async Task<Department> GetByIdAsync(int id)
    {
        return await _dbSet
                   .Where(d => !d.IsDeleted && d.Id == id)
                   .Include(d => d.Expenses)
                   .Include(d => d.Workers)
                   .AsSplitQuery()
                   .FirstOrDefaultAsync()
               ?? throw new InvalidOperationException($"No {nameof(Department)} found with Id {id} in the database.");
    }

    
    public async Task DeleteAsync(Department department) 
    { 
        department.IsDeleted = true; 
        
        foreach (var worker in department.Workers) 
        { 
            worker.IsDeleted = true; 
        }

        foreach (var expense in department.Expenses)
        {
            expense.IsDeleted = true;  
        }
        
        await _context.SaveChangesAsync();
    }
}