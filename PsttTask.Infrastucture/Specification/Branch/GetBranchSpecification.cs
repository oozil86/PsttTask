using Microsoft.EntityFrameworkCore;
using PsttTask.Domain.Contracts;
using PsttTask.Domain.ISpecification.Branch;
using PsttTask.Infrastructure.Data;

namespace PsttTask.Infrastucture.Specification.Branch;

public class GetBranchSpecification(PsttTaskContext context) : EFSpecification(context), IGetBranchSpecification, ScopedInjectable
{
    private Guid _branchReference;

    public async Task<Domain.Entities.Branch?> Query(CancellationToken cancellationToken = default)
        => await Context
        .Set<Domain.Entities.Branch>()
        .Include(c => c.Company)
        .FirstOrDefaultAsync(c => c.Reference == _branchReference, cancellationToken: cancellationToken);

    public void SetBranchReference(Guid branchReference)
    {
        _branchReference = branchReference;
    }
}
