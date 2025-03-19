using Company_Expense_Tracker.Dtos.ExpenseDtos;
using Company_Expense_Tracker.Entities;
using Company_Expense_Tracker.Repositories;

namespace Company_Expense_Tracker.Services.ExpenseService;

public class ExpenseService : IExpenseService
{
    private readonly IExpenseRepository _repository;
    private readonly IDepartmentRepository _departmentRepository;

    public ExpenseService(IExpenseRepository repository, IDepartmentRepository departmentRepository)
    {
        _repository = repository;
        _departmentRepository = departmentRepository;
    }

    public async Task<List<ExpenseDto>> GetAllAsync()
    {
        var expenses = await _repository.GetAllAsync();

        var dto = expenses.Where(e => !e.IsDeleted)
            .Select(e => new ExpenseDto()
            {   
                Id = e.Id,
                Amount = e.Amount,
                Currency = e.Currency,
                PaymentMethod = e.PaymentMethod,
                Catagory = e.Catagory,
                DepartmentId = e.DepartmentId ,
                Description = e.Description
            }).ToList();
        
        return dto;
    }

    public async Task<ExpenseDto> GetByIdAsync(int id)
    {
        var expense = await _repository.GetByIdAsync(id);
        
        var dto = new ExpenseDto
        {
            Id = expense.Id,
            Amount = expense.Amount,
            Currency = expense.Currency,
            PaymentMethod = expense.PaymentMethod,
            Catagory = expense.Catagory,
            DepartmentId = expense.DepartmentId,
            Description = expense.Description
        };

        return dto;
    }

    public async Task<int> CreateAsync(CreateExpenseDto createDto)
    {
        var departament = await _departmentRepository.GetByIdAsync(createDto.DepartmentId);
        
        var expense = new Expense()
        {
            Amount = createDto.Amount,
            Currency = createDto.Currency,
            PaymentMethod = createDto.PaymentMethod,
            Catagory = createDto.Catagory,
            DepartmentId = createDto.DepartmentId,
            Description = createDto.Description
        };
        
        await _repository.CreateAsync(expense);
        return expense.Id;
    }

    public async Task UpdateAsync(UpdateExpenseDto updateDto)
    {
        var expense = await _repository.GetByIdAsync(updateDto.Id);
        var departament = await _departmentRepository.GetByIdAsync(updateDto.DepartmentId);

        expense.Amount = updateDto.Amount;
        expense.Currency = updateDto.Currency;
        expense.PaymentMethod = updateDto.PaymentMethod;
        expense.Catagory = updateDto.Catagory;
        expense.DepartmentId = updateDto.DepartmentId;
        expense.Description = updateDto.Description;

        await _repository.UpdateAsync(expense);
    }

    public async Task DeleteAsync(int id)
    {
        var expense = await _repository.GetByIdAsync(id);
        await _repository.DeleteAsync(expense);
    }
}