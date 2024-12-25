using AutoMapper;
using MediatR;
using PsttTask.Domain.Data;
using PsttTask.Domain.ISpecification.Company;
using PsttTask.Domain.Models.Branch;

namespace PsttTask.ApplicationService.Features.Branch;

public record SaveBranchCommand(SaveBranchModel Branch) : IRequest<Guid>;

public class SaveBranchCommandHandler(IGetCompanySpecification getCompanySpecification
    , IMapper mapper
    , IGenericRepository<Domain.Entities.Branch, Guid> genericRepository
    , IPsttTaskUnitOfWork PsttTaskUnitOfWork
    ) : IRequestHandler<SaveBranchCommand, Guid>
{

    public async Task<Guid> Handle(SaveBranchCommand request, CancellationToken cancellationToken)
    {
        var company = await ValidateCompany(request.Branch.CompanyReference, cancellationToken);
        var targetBranch = new Domain.Entities.Branch(request.Branch.Name, company.Id);
        await genericRepository.AddAsync(targetBranch);
        await PsttTaskUnitOfWork.SaveAsync(cancellationToken);
        return targetBranch.Reference;
    }

    private async Task<Domain.Entities.Company> ValidateCompany(Guid companyReference, CancellationToken cancellationToken)
    {
        getCompanySpecification.SetCompanyReference(companyReference);
        var existedCompany = await getCompanySpecification.Query(cancellationToken);
        return existedCompany is null ? throw new Exception("This Email Is Not Valid!!") : existedCompany;
    }
}
