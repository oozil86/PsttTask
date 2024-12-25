using AutoMapper;
using MediatR;
using PsttTask.Domain.Contracts;
using PsttTask.Domain.ISpecification.Company;
using PsttTask.Domain.Models.Company;

namespace PsttTask.ApplicationService.Features.Company;

public record GetPagedCompaniesQuery(PageFilter filter) : IRequest<PageList<CompanyModel>>;

public class GetPagedCompaniesQueryHandler(IGetPagedCompaniesSpecification getPagedCompaniesSpecification, IMapper mapper) : IRequestHandler<GetPagedCompaniesQuery, PageList<CompanyModel>>
{
    public async Task<PageList<CompanyModel>> Handle(GetPagedCompaniesQuery request, CancellationToken cancellationToken)
    {
        getPagedCompaniesSpecification.SetPageFilter(request.filter);
        var companies = await getPagedCompaniesSpecification.Query(cancellationToken);
        var mappedCompanies = mapper.Map<List<CompanyModel>>(companies.Data);
        return new PageList<CompanyModel>(mappedCompanies, companies.Count);
    }
}
