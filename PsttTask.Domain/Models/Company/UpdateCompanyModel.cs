namespace PsttTask.Domain.Models.Company;

public record UpdateCompanyModel
{
    public Guid Reference { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}
