using PsttTask.Domain.Contract;
namespace PsttTask.Domain.ISpecification.Branch;

public interface IGetBranchesSpecification : IAsyncSpecification<List<Entities.Branch>>
{
}
