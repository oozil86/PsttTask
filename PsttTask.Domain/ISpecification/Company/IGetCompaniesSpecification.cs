using PsttTask.Domain.Contract;
namespace PsttTask.Domain.ISpecification.Company;

public interface IGetCompaniesSpecification : IAsyncSpecification<List<Entities.Company>>
{
}
