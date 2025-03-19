using Company_Expense_Tracker.Entities;

namespace Company_Expense_Tracker.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User> GetUserByMail(string email);
}