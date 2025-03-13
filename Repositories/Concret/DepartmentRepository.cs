using Company_Expense_Tracker.DataAccess;
using Company_Expense_Tracker.Entities;

namespace Company_Expense_Tracker.Repositories.Concret;

public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
{
    public DepartmentRepository(DataContext dataContext) : base(dataContext)
    {
    }
}