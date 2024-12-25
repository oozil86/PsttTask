using AutoMapper;
using MediatR;
using PsttTask.Domain.ISpecification.Branch;
using PsttTask.Domain.Models.Branch;

namespace PsttTask.ApplicationService.Features.Branch;

public record GetBranchesQuery : IRequest<List<BranchModel>>;

public class GetBranchesQueryHandler(IGetBranchesSpecification getBranchesSpecification, IMapper mapper) : IRequestHandler<GetBranchesQuery, List<BranchModel>>
{
    public async Task<List<BranchModel>> Handle(GetBranchesQuery request, CancellationToken cancellationToken)
    {
        var branches = await getBranchesSpecification.Query(cancellationToken);
        return mapper.Map<List<BranchModel>>(branches);
    }
}
