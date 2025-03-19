using Company_Expense_Tracker.DataAccess;
using Company_Expense_Tracker.Entities;
using Microsoft.EntityFrameworkCore;

namespace Company_Expense_Tracker.Repositories.Concret;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    private readonly DbSet<User> _dbSet;
    
    public UserRepository(DataContext dataContext) : base(dataContext)
    {
        _dbSet = dataContext.Set<User>();  
    }
    
    public async Task<User?> GetUserByMail(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Email.Equals(email));
    }
}