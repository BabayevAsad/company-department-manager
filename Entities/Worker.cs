using System.Runtime.InteropServices.JavaScript;

namespace Company_Expense_Tracker.Entities;

public class Worker : BaseEntity
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
    public Department Department { get; set; }
}