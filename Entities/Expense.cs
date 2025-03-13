namespace Company_Expense_Tracker.Entities;

public class Expense : BaseEntity
{
    public int Amount { get; set; }  
    public string Currency { get; set; }
    public string PaymentMethod { get; set; }
    public string Catagory { get; set; }
    public DateTime ExpenseDate { get; set; }
    public int DepartmentId { get; set; }
    public Department Department { get; set; }
    public string Description { get; set; }
}