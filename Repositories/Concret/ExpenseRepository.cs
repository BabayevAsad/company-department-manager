using Company_Expense_Tracker.DataAccess;
using Company_Expense_Tracker.Entities;

namespace Company_Expense_Tracker.Repositories.Concret;

public class ExpenseRepository : BaseRepository<Expense>, IExpenseRepository
{
    public ExpenseRepository(DataContext dataContext) : base(dataContext)
    {
    }
}