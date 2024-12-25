using FluentValidation;
using PsttTask.Domain.Models.Company;

namespace PsttTask.ApplicationService.Features.Company;

public class EditCompanyCommandValidator : AbstractValidator<UpdateCompanyModel>
{
    public EditCompanyCommandValidator()
    {
        RuleFor(c => c.Reference)
         .NotEmpty().WithMessage("Reference Can Not Be Empty")
         .NotNull().WithMessage("Reference Can Not Be Null");

        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Name Can Not Be Empty")
            .NotNull().WithMessage("Name Can Not Be Null");

        RuleFor(c => c.Email)
            .NotEmpty().WithMessage("Email Can Not Be Empty")
            .NotNull().WithMessage("Email Can Not Be Null");

        RuleFor(c => c.Phone)
            .NotEmpty().WithMessage("Phone Can Not Be Empty")
            .NotNull().WithMessage("Phone Can Not Be Null");

    }
}
