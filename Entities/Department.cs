namespace Company_Expense_Tracker.Entities;

public class Department : BaseEntity
{
    public string Name { get; set; }
    public int DepartmentId { get; set; }
    public List<Expense> Expenses { get; set; }
    public List<Worker> Workers { get; set; }
}