namespace PsttTask.Domain.Models.Branch;

public record BranchModel
{
    public Guid Reference { set; get; }
    public string Name { set; get; }
    public Guid CompanyReference { set; get; }
}
