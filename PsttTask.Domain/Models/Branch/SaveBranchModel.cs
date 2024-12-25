namespace PsttTask.Domain.Models.Branch;

public record SaveBranchModel
{
    public string Name { get; set; }
    public Guid CompanyReference { set; get; }
}
