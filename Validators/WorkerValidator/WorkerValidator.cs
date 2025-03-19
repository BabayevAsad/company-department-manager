using Company_Expense_Tracker.Dtos.WorkerDtos;
using FluentValidation;

namespace Company_Expense_Tracker.Validators.WorkerValidator;

public class WorkerValidator<T> : AbstractValidator<T> where T : ActionWorkerDto
{
    protected WorkerValidator()
    {
        RuleFor(w => w.GenderId)
            .Must(id => id == 1 || id == 2)
            .WithMessage("GenderId must be either 1 (Male) or 2 (Female).");

        RuleFor(w => w.Email)
            .NotEmpty().WithMessage("Email is required.")
            .Matches(@"^[^@\s]+@(gmail\.com|mail\.ru|outlook\.com|yahoo\.com|icloud\.com)$")
            .WithMessage(
                "Email must be from one of the following domains: gmail.com, mail.ru, outlook.com, yahoo.com, icloud.com.");

        RuleFor(w => w.PhoneNumber)
            .NotEmpty().WithMessage("Phone Number is required.")
            .Matches(@"^\+\d{1,3}\d{6,14}$")
            .WithMessage("Phone Number must start with '+' followed by a valid country code and number.");

        RuleFor(w => w.BirthDate)
            .NotEmpty().WithMessage("Birth Date is required.")
            .Must(date => date != default(DateTime))
            .WithMessage("Birth Date must be a valid DateTime format.");

        RuleFor(w => w.FatherName)
            .NotEmpty().WithMessage("Father Name is required.");

        RuleFor(w => w.FinNumber)
            .NotEmpty().WithMessage("FIN Number is required.");

        RuleFor(w => w.Nationality)
            .NotEmpty().WithMessage("Nationality is required.");

        RuleFor(w => w.Name)
            .NotEmpty().WithMessage("Name is required.");

        RuleFor(w => w.Surname)
            .NotEmpty().WithMessage("Surname is required.");
    }
}
    
