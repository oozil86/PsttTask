using AutoMapper;
using MediatR;
using PsttTask.Domain.Data;
using PsttTask.Domain.ISpecification.Company;
using PsttTask.Domain.Models.Company;

namespace PsttTask.ApplicationService.Features.Company;

public record EditCompanyCommand(UpdateCompanyModel company) : IRequest<bool>;

public class UpdateCompanyCommandHandler(IGetCompanyByEmailSpecification getCompanyByEmailSpecification
    , IGetCompanySpecification getCompanySpecification
    , IMapper mapper
    , IGenericRepository<Domain.Entities.Company, Guid> genericRepository
    , IPsttTaskUnitOfWork PsttTaskUnitOfWork
    ) : IRequestHandler<EditCompanyCommand, bool>
{

    public async Task<bool> Handle(EditCompanyCommand request, CancellationToken cancellationToken)
    {
        var Company = await ValidateCompanyReference(request.company, cancellationToken);
        await ValidateCompanyEmail(request.company, cancellationToken);

        Company.Update(request.company.Name, request.company.Email, request.company.Phone);
        genericRepository.Update(Company);
        await PsttTaskUnitOfWork.SaveAsync(cancellationToken);
        return true;
    }

    private async Task<Domain.Entities.Company> ValidateCompanyReference(UpdateCompanyModel Company, CancellationToken cancellationToken)
    {
        getCompanySpecification.SetCompanyReference(Company.Reference);
        return await getCompanySpecification.Query(cancellationToken) ?? throw new Exception("This Company Is Not Exists!!");
    }

    private async Task ValidateCompanyEmail(UpdateCompanyModel Company, CancellationToken cancellationToken)
    {
        getCompanyByEmailSpecification.SetCompanyEmail(Company.Email);
        var existedCompany = await getCompanyByEmailSpecification.Query(cancellationToken);
        if (existedCompany is not null && existedCompany.Reference != Company.Reference)
            throw new Exception("This Email Is Not Valid!!");
    }

}
