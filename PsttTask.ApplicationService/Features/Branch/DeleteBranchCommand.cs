using MediatR;
using PsttTask.Domain.Data;
using PsttTask.Domain.ISpecification.Branch;

namespace PsttTask.ApplicationService.Features.Branch;

public record DeleteBranchCommand(Guid reference) : IRequest<bool>;

public class DeleteBranchCommandHandler(IGetBranchSpecification getBranchSpecification
    , IPsttTaskUnitOfWork PsttTaskUnitOfWork) : IRequestHandler<DeleteBranchCommand, bool>
{

    public async Task<bool> Handle(DeleteBranchCommand request, CancellationToken cancellationToken)
    {
        getBranchSpecification.SetBranchReference(request.reference);
        var Branch = await getBranchSpecification.Query(cancellationToken) ?? throw new Exception("This Branch Not Found.");
        Branch.SoftDelete();
        await PsttTaskUnitOfWork.SaveAsync(cancellationToken);
        return true;
    }
}
