using AutoMapper;
using MediatR;
using PsttTask.Domain.Contracts;
using PsttTask.Domain.ISpecification.Branch;
using PsttTask.Domain.Models.Branch;

namespace PsttTask.ApplicationService.Features.Branch;

public record GetPagedBranchesQuery(PageFilter filter) : IRequest<PageList<BranchModel>>;

public class GetPagedBranchesQueryHandler(IGetPagedBranchesSpecification getPagedBranchesSpecification, IMapper mapper) : IRequestHandler<GetPagedBranchesQuery, PageList<BranchModel>>
{
    public async Task<PageList<BranchModel>> Handle(GetPagedBranchesQuery request, CancellationToken cancellationToken)
    {
        getPagedBranchesSpecification.SetPageFilter(request.filter);
        var branches = await getPagedBranchesSpecification.Query(cancellationToken);
        var mappedBranches = mapper.Map<List<BranchModel>>(branches.Data);
        return new PageList<BranchModel>(mappedBranches, branches.Count);
    }
}
