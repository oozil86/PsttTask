using AutoMapper;
using MediatR;
using PsttTask.Domain.ISpecification.Company;
using PsttTask.Domain.Models.Company;

namespace PsttTask.ApplicationService.Features.Company;

public record GetCompaniesQuery : IRequest<List<CompanyModel>>;

public class GetCompaniesQueryHandler(IGetCompaniesSpecification getCompaniesSpecification, IMapper mapper) : IRequestHandler<GetCompaniesQuery, List<CompanyModel>>
{
    public async Task<List<CompanyModel>> Handle(GetCompaniesQuery request, CancellationToken cancellationToken)
    {
        var companies = await getCompaniesSpecification.Query(cancellationToken);
        return mapper.Map<List<CompanyModel>>(companies);
    }
}
