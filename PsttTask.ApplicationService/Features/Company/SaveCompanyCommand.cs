using AutoMapper;
using MediatR;
using PsttTask.Domain.Data;
using PsttTask.Domain.ISpecification.Company;
using PsttTask.Domain.Models.Company;

namespace PsttTask.ApplicationService.Features.Company;

public record SaveCompanyCommand(SaveCompanyModel Company) : IRequest<Guid>;

public class SaveCompanyCommandHandler(IGetCompanyByEmailSpecification getCompanyByEmailSpecification
    , IMapper mapper
    , IGenericRepository<Domain.Entities.Company, Guid> genericRepository
    , IPsttTaskUnitOfWork PsttTaskUnitOfWork
    ) : IRequestHandler<SaveCompanyCommand, Guid>
{

    public async Task<Guid> Handle(SaveCompanyCommand request, CancellationToken cancellationToken)
    {
        getCompanyByEmailSpecification.SetCompanyEmail(request.Company.Email);
        var Company = await getCompanyByEmailSpecification.Query(cancellationToken);
        if (Company is not null)
            throw new Exception("This Company Is Already Exists!!");

        var targetCompany = mapper.Map<Domain.Entities.Company>(request.Company);
        await genericRepository.AddAsync(targetCompany);
        await PsttTaskUnitOfWork.SaveAsync(cancellationToken);
        return targetCompany.Reference;
    }
}
