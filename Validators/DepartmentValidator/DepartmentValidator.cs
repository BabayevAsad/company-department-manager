using Company_Expense_Tracker.Dtos.DepartmentDtos;
using FluentValidation;

namespace Company_Expense_Tracker.Validators.DepartmentValidator;

public class DepartmentValidator<T> : AbstractValidator<T> where T : ActionDepartmentDto
{
    protected DepartmentValidator()
    {
        RuleFor(d => d.Name)
            .NotEmpty().WithMessage("Name cannot be empty!");
    }
}