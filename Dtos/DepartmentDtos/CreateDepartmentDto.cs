namespace Company_Expense_Tracker.Dtos.DepartmentDtos;

public class CreateDepartmentDto : ActionDto
{
    public string Name { get; set; }
    public int DepartmentId { get; set; }
}