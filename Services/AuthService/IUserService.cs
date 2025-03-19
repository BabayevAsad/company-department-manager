using Company_Expense_Tracker.Dtos.UserDtos;

namespace Company_Expense_Tracker.Services.AuthService;

public interface IUserService
{
    Task<string> RegisterUser(RegisterUserDto registerUserDto);
    Task<string> LoginUser(LoginUserDto loginUserDto);
}