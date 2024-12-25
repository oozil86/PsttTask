using PsttTask.Domain.Contracts;

namespace PsttTask.Domain.Entities;

public class Branch : AggregateRoot<long>
{
    public string Name { get; private set; }
    public long CompanyId { get; private set; }
    public Company Company { get; private set; }

    private Branch() { }

    public Branch(string name, long companyId)
    {
        UpdateName(name);
        UpdateCompanyId(companyId);
    }

    public string GetName() => Name;
    public void UpdateName(string name)
    {
        Name = name;
    }
    public void UpdateCompanyId(long companyId)
    {
        CompanyId = companyId;
    }
    public void Update(string name, long companyId)
    {
        UpdateName(name);
        UpdateCompanyId(companyId);
    }

}

