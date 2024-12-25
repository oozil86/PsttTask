using Microsoft.EntityFrameworkCore;
using PsttTask.Domain.Contracts;
using PsttTask.Domain.ISpecification.Branch;
using PsttTask.Infrastructure.Data;


namespace PsttTask.Infrastucture.Specification.Branch;

public class GetBranchesSpecification(PsttTaskContext context) : EFSpecification(context), IGetBranchesSpecification, ScopedInjectable
{
    public async Task<List<Domain.Entities.Branch>> Query(CancellationToken cancellationToken = default)
        => await Context
        .Set<Domain.Entities.Branch>()
        .Include(c => c.Company)
        .ToListAsync(cancellationToken: cancellationToken);
}
