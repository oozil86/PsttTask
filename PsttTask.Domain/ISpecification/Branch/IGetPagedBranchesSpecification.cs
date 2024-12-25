using PsttTask.Domain.Contract;
using PsttTask.Domain.Contracts;
namespace PsttTask.Domain.ISpecification.Branch;

public interface IGetPagedBranchesSpecification : IAsyncSpecification<PageList<Entities.Branch>>
{
    public void SetPageFilter(PageFilter filter);
}
