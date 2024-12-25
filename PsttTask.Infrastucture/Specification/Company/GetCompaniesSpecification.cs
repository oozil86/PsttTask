using Microsoft.EntityFrameworkCore;
using PsttTask.Domain.Contracts;
using PsttTask.Domain.ISpecification.Company;
using PsttTask.Infrastructure.Data;


namespace PsttTask.Infrastucture.Specification.Company;

public class GetCompaniesSpecification(PsttTaskContext context) : EFSpecification(context), IGetCompaniesSpecification, ScopedInjectable
{
    public async Task<List<Domain.Entities.Company>> Query(CancellationToken cancellationToken = default)
        => await Context
        .Set<Domain.Entities.Company>()
        .Include(c => c.Branch)
        .ToListAsync(cancellationToken: cancellationToken);
}
