namespace Company_Expense_Tracker.Entities;

public class Department : BaseEntity
{
    public string Name { get; set; }
    public List<Expense> Expenses { get; set; } = new List<Expense>();
    public List<Worker> Workers { get; set; } = new List<Worker>();
}