using PsttTask.Domain.Contract;

namespace PsttTask.Domain.ISpecification.Company;

public interface IGetCompanyByEmailSpecification : IAsyncSpecification<Entities.Company?>
{
    public void SetCompanyEmail(string companyEmail);
}
