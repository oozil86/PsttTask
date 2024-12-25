using AutoMapper;
using MediatR;
using PsttTask.Domain.ISpecification.Branch;
using PsttTask.Domain.Models.Branch;

namespace PsttTask.ApplicationService.Features.Branch;

public record GetBranchQuery(Guid BranchReference) : IRequest<BranchModel>;

public class GetBranchQueryHandler(IGetBranchSpecification getBranchSpecification, IMapper mapper) : IRequestHandler<GetBranchQuery, BranchModel>
{
    public async Task<BranchModel> Handle(GetBranchQuery request, CancellationToken cancellationToken)
    {
        getBranchSpecification.SetBranchReference(request.BranchReference);
        var Branch = await getBranchSpecification.Query(cancellationToken);
        return mapper.Map<BranchModel>(Branch);
    }
}
