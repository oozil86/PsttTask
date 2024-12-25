using Microsoft.EntityFrameworkCore;
using PsttTask.Domain.Contracts;
using PsttTask.Domain.ISpecification.Company;
using PsttTask.Infrastructure.Data;

namespace PsttTask.Infrastucture.Specification.Company;

public class GetCompanyByEmailSpecification(PsttTaskContext context) : EFSpecification(context), IGetCompanyByEmailSpecification, ScopedInjectable
{
    private string? _email;

    public async Task<Domain.Entities.Company?> Query(CancellationToken cancellationToken = default)
        => await Context
        .Set<Domain.Entities.Company>()
        .Include(c => c.Branch)
        .FirstOrDefaultAsync(c => c.Email == _email, cancellationToken: cancellationToken);


    public void SetCompanyEmail(string companyEmail)
    {
        _email = companyEmail;
    }
}
