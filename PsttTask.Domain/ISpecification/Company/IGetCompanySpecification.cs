using PsttTask.Domain.Contract;

namespace PsttTask.Domain.ISpecification.Company;

public interface IGetCompanySpecification : IAsyncSpecification<Entities.Company?>
{
    public void SetCompanyReference(Guid CompanyReference);
}
