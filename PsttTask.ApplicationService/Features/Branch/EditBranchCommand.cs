using AutoMapper;
using MediatR;
using PsttTask.Domain.Data;
using PsttTask.Domain.ISpecification.Branch;
using PsttTask.Domain.ISpecification.Company;
using PsttTask.Domain.Models.Branch;

namespace PsttTask.ApplicationService.Features.Branch;

public record EditBranchCommand(UpdateBranchModel Branch) : IRequest<bool>;

public class UpdateBranchCommandHandler(IGetCompanySpecification getCompanySpecification
    , IGetBranchSpecification getBranchSpecification
    , IMapper mapper
    , IGenericRepository<Domain.Entities.Branch, Guid> genericRepository
    , IPsttTaskUnitOfWork PsttTaskUnitOfWork
    ) : IRequestHandler<EditBranchCommand, bool>
{

    public async Task<bool> Handle(EditBranchCommand request, CancellationToken cancellationToken)
    {
        var Branch = await ValidateBranchReference(request.Branch, cancellationToken);
        var company = await ValidateCompany(request.Branch.CompanyReference, cancellationToken);

        Branch.Update(request.Branch.Name, company.Id);
        genericRepository.Update(Branch);
        await PsttTaskUnitOfWork.SaveAsync(cancellationToken);
        return true;
    }

    private async Task<Domain.Entities.Branch> ValidateBranchReference(UpdateBranchModel Branch, CancellationToken cancellationToken)
    {
        getBranchSpecification.SetBranchReference(Branch.Reference);
        return await getBranchSpecification.Query(cancellationToken) ?? throw new Exception("This Branch Is Not Exists!!");
    }

    private async Task<Domain.Entities.Company> ValidateCompany(Guid companyReference, CancellationToken cancellationToken)
    {
        getCompanySpecification.SetCompanyReference(companyReference);
        var existedCompany = await getCompanySpecification.Query(cancellationToken);
        return existedCompany is null ? throw new Exception("This Email Is Not Valid!!") : existedCompany;
    }

}
