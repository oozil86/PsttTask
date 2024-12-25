using FluentValidation;
using PsttTask.Domain.Models.Branch;

namespace PsttTask.ApplicationService.Features.Branch;

public class SaveBranchCommandValidator : AbstractValidator<SaveBranchModel>
{
    public SaveBranchCommandValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Name Can Not Be Empty")
            .NotNull().WithMessage("Name Can Not Be Null");

        RuleFor(c => c.CompanyReference)
              .NotEmpty().WithMessage("CompanyReference Can Not Be Empty")
              .NotNull().WithMessage("CompanyReference Can Not Be Null");
    }
}
