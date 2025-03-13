namespace Company_Expense_Tracker.Entities;

public enum Role
{
    Administrator = 1,
    FinanceManager = 2,
    DepartmentHead = 3,
    Employee = 4,
    Auditor = 5 
}

public static class RoleHelper
{
    public static Role GetById(int roleId)
    {
        return (Role)roleId;
    }
}