using Company_Expense_Tracker.Dtos.UserDtos;
using FluentValidation;

namespace Company_Expense_Tracker.Validators.UserValidator;

public class UserValidator<T> : AbstractValidator<T> where T: UserDto
{
    protected UserValidator()
    {
        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("Email is required.")
            .Matches(@"^[^@\s]+@(gmail\.com|mail\.ru|outlook\.com|yahoo\.com|icloud\.com)$")
            .WithMessage("Email must be from one of the following domains: gmail.com, mail.ru, outlook.com, yahoo.com, icloud.com.");

        RuleFor(u => u.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.");
    } 
}