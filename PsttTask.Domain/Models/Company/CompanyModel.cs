namespace PsttTask.Domain.Models.Company;

public record CompanyModel
{
    public string Name { set; get; }
    public string Email { set; get; }
    public string Phone { set; get; }
    public Guid Reference { set; get; }
}
