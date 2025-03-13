namespace Company_Expense_Tracker.Dtos.ExpenseDtos;

public class ExpenseDto : BaseDto
{
    public int Amount { get; set; }  
    public string Currency { get; set; }
    public string PaymentMethod { get; set; }
    public string Catagory { get; set; }
    public int DepartmentId { get; set; }
    public string Description { get; set; }
}