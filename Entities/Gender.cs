namespace Company_Expense_Tracker.Entities;

public enum Gender
{
    Male = 1,
    Female = 2,
}

public static class GenderHelper
{
    public static Gender? GetById(int genderId)
    {
        if (Enum.IsDefined(typeof(Gender), genderId))
        {
            return (Gender)genderId;
        }
        return null;
    }
}
