using Company_Expense_Tracker.Dtos.ExpenseDtos;
using Company_Expense_Tracker.Dtos.WorkerDtos;

namespace Company_Expense_Tracker.Dtos.DepartmentDtos;

public class DepartmentDetailsDto : DepartmentDto
{
    public List<ExpenseDto> Expenses { get; set; }
    public List<WorkerDto> Workers { get; set; }
}