using AutoMapper;
using MediatR;
using PsttTask.Domain.ISpecification.Company;
using PsttTask.Domain.Models.Company;

namespace PsttTask.ApplicationService.Features.Company;

public record GetCompanyQuery(Guid CompanyReference) : IRequest<CompanyModel>;

public class GetCompanyQueryHandler(IGetCompanySpecification getCompanySpecification, IMapper mapper) : IRequestHandler<GetCompanyQuery, CompanyModel>
{
    public async Task<CompanyModel> Handle(GetCompanyQuery request, CancellationToken cancellationToken)
    {
        getCompanySpecification.SetCompanyReference(request.CompanyReference);
        var company = await getCompanySpecification.Query(cancellationToken);
        return mapper.Map<CompanyModel>(company);
    }
}
