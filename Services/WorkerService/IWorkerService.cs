using Company_Expense_Tracker.Dtos.WorkerDtos;

namespace Company_Expense_Tracker.Services.WorkerService;

public interface IWorkerService : IBaseService<WorkerDto,CreateWorkerDto,UpdateWorkerDto>
{
}