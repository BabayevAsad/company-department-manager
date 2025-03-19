using Company_Expense_Tracker.Dtos.ExpenseDtos;
using FluentValidation;

namespace Company_Expense_Tracker.Validators.ExpenseValidator;

public class ExpenseValidator<T> : AbstractValidator<T> where T : ActionExpenseDto
{
    protected ExpenseValidator()
    {
        RuleFor(e => e.PaymentMethod)
            .NotEmpty().WithMessage("Payment Method is required.")
            .Must(method => method == "ByCash" || method == "ByCard")
            .WithMessage("Payment Method must be either 'ByCash' or 'ByCard'.");

        RuleFor(e => e.Description)
            .NotEmpty().WithMessage("Description is required.");
            
        RuleFor(e => e.Catagory)
            .NotEmpty().WithMessage("Category is required.");

        RuleFor(e => e.DepartmentId)
            .NotNull().WithMessage("Department ID is required.")
            .GreaterThan(0).WithMessage("Department ID must be greater than 0.");

        RuleFor(e => e.Currency)
            .NotEmpty().WithMessage("Currency is required.")
            .Must(currency => new[] { "Dollar", "Euro", "Pound", "Yen", "Ruble", "Lira", "Dirham", "Rupee", "Yuan" }
                .Contains(currency))
            .WithMessage("Currency must be one of the following: Dollar, Euro, Pound, Yen, Ruble, Lira, Dirham, Rupee, or Yuan.");

        RuleFor(e => e.Amount)
            .InclusiveBetween(1, 10_000_000)
            .WithMessage("Amount must be between 0 and 10,000,000.");
    }
}