using Microsoft.EntityFrameworkCore;
using PsttTask.Domain.Contracts;
using PsttTask.Domain.ISpecification.Company;
using PsttTask.Infrastructure.Data;

namespace PsttTask.Infrastucture.Specification.Company;

public class GetCompanySpecification(PsttTaskContext context) : EFSpecification(context), IGetCompanySpecification, ScopedInjectable
{
    private Guid _CompanyReference;

    public async Task<Domain.Entities.Company?> Query(CancellationToken cancellationToken = default)
        => await Context
        .Set<Domain.Entities.Company>()
        .Include(c => c.Branch)
        .FirstOrDefaultAsync(c => c.Reference == _CompanyReference, cancellationToken: cancellationToken);

    public void SetCompanyReference(Guid CompanyReference)
    {
        _CompanyReference = CompanyReference;
    }
}
