namespace PsttTask.Domain.Models.Branch;

public record UpdateBranchModel
{
    public Guid Reference { get; set; }
    public string Name { get; set; }
    public Guid CompanyReference { set; get; }
}
