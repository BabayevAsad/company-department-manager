using Company_Expense_Tracker.Dtos.ExpenseDtos;

namespace Company_Expense_Tracker.Services.ExpenseService;

public interface IExpenseService : IBaseService<ExpenseDto,CreateExpenseDto,UpdateExpenseDto>
{
}