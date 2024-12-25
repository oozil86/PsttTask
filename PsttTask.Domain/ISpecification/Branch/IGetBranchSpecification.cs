using PsttTask.Domain.Contract;

namespace PsttTask.Domain.ISpecification.Branch;

public interface IGetBranchSpecification : IAsyncSpecification<Entities.Branch?>
{
    public void SetBranchReference(Guid branchReference);
}
