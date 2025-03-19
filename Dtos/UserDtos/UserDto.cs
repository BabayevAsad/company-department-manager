namespace Company_Expense_Tracker.Dtos.UserDtos;

public class UserDto : ActionDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}