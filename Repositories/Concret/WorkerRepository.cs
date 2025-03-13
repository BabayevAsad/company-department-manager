using Company_Expense_Tracker.DataAccess;
using Company_Expense_Tracker.Entities;

namespace Company_Expense_Tracker.Repositories.Concret;

public class WorkerRepository : BaseRepository<Worker>, IWorkerRepository
{
    public WorkerRepository(DataContext dataContext) : base(dataContext)
    {
    }
}