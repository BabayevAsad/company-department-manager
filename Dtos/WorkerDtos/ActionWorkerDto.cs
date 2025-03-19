namespace Company_Expense_Tracker.Dtos.WorkerDtos;

public class ActionWorkerDto : ActionDto
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string FatherName { get; set; }
    public DateTime BirthDate { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Nationality { get; set; }
    public string FinNumber { get; set; }
    public int GenderId { get; set; }
    public int DepartmentId { get; set; }
}