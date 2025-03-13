using Company_Expense_Tracker.Dtos.DepartmentDtos;

namespace Company_Expense_Tracker.Services.DepartmentService;

public interface IDepartmentService : IBaseService<DepartmentDto,CreateDepartmentDto,UpdateDepartmentDto>
{
}