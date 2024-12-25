using PsttTask.Domain.Contract;
using PsttTask.Domain.Contracts;
namespace PsttTask.Domain.ISpecification.Company;

public interface IGetPagedCompaniesSpecification : IAsyncSpecification<PageList<Entities.Company>>
{
    public void SetPageFilter(PageFilter filter);
}
