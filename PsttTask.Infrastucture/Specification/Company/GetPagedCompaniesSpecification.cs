using Microsoft.EntityFrameworkCore;
using PsttTask.Domain.Contracts;
using PsttTask.Domain.Enums;
using PsttTask.Domain.ISpecification.Company;
using PsttTask.Infrastructure.Data;


namespace PsttTask.Infrastucture.Specification.Company;

public class GetPagedCompaniesSpecification(PsttTaskContext context) : EFSpecification(context), IGetPagedCompaniesSpecification, ScopedInjectable
{
    private PageFilter _filter;
    public async Task<PageList<Domain.Entities.Company>> Query(CancellationToken cancellationToken = default)
    {
        IQueryable<Domain.Entities.Company> query = Context
        .Set<Domain.Entities.Company>()
        .AsQueryable();

        var count = await query.CountAsync(cancellationToken: cancellationToken);
        query = _filter.OrderType == OrderType.Asc ? query.OrderBy(c => c.Name) : query.OrderByDescending(c => c.Name);
        var data = await query.Skip(_filter.PageIndex).Take(_filter.PageSize).ToListAsync(cancellationToken: cancellationToken);


        return new PageList<Domain.Entities.Company>(data, count);
    }
    public void SetPageFilter(PageFilter filter)
    {
        _filter = filter;
    }
}
