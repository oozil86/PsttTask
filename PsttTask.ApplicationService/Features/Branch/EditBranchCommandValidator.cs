using FluentValidation;
using PsttTask.Domain.Models.Branch;

namespace PsttTask.ApplicationService.Features.Branch;

public class EditBranchCommandValidator : AbstractValidator<UpdateBranchModel>
{
    public EditBranchCommandValidator()
    {
        RuleFor(c => c.Reference)
         .NotEmpty().WithMessage("Reference Can Not Be Empty")
         .NotNull().WithMessage("Reference Can Not Be Null");

        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Name Can Not Be Empty")
            .NotNull().WithMessage("Name Can Not Be Null");

        RuleFor(c => c.CompanyReference)
            .NotEmpty().WithMessage("CompanyReference Can Not Be Empty")
            .NotNull().WithMessage("CompanyReference Can Not Be Null");


    }
}
