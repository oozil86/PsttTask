using FluentValidation;
using PsttTask.Domain.Models.Company;

namespace PsttTask.ApplicationService.Features.Company;

public class SaveCustomerCommandValidator : AbstractValidator<SaveCompanyModel>
{
    public SaveCustomerCommandValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Name Can Not Be Empty")
            .NotNull().WithMessage("Name Can Not Be Null");

        RuleFor(c => c.Email)
              .NotEmpty().WithMessage("Email Can Not Be Empty")
              .NotNull().WithMessage("Email Can Not Be Null");
    }
}
