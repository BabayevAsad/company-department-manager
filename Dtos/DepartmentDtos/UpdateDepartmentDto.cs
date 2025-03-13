namespace Company_Expense_Tracker.Dtos.DepartmentDtos;

public class UpdateDepartmentDto : ActionDto
{
    public string Name { get; set; }
    public int DepartmentId { get; set; }
}