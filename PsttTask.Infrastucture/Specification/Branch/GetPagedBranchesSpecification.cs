using Microsoft.EntityFrameworkCore;
using PsttTask.Domain.Contracts;
using PsttTask.Domain.Enums;
using PsttTask.Domain.ISpecification.Branch;
using PsttTask.Infrastructure.Data;


namespace PsttTask.Infrastucture.Specification.Branch;

public class GetPagedBranchesSpecification(PsttTaskContext context) : EFSpecification(context), IGetPagedBranchesSpecification, ScopedInjectable
{
    private PageFilter _filter;
    public async Task<PageList<Domain.Entities.Branch>> Query(CancellationToken cancellationToken = default)
    {
        IQueryable<Domain.Entities.Branch> query = Context
        .Set<Domain.Entities.Branch>()
        .AsQueryable();

        var count = await query.CountAsync(cancellationToken: cancellationToken);
        query = _filter.OrderType == OrderType.Asc ? query.OrderBy(c => c.Name) : query.OrderByDescending(c => c.Name);
        var data = await query.Skip(_filter.PageIndex).Take(_filter.PageSize).ToListAsync(cancellationToken: cancellationToken);


        return new PageList<Domain.Entities.Branch>(data, count);
    }
    public void SetPageFilter(PageFilter filter)
    {
        _filter = filter;
    }
}
