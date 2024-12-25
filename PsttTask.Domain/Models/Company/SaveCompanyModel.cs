namespace PsttTask.Domain.Models.Company;

public record SaveCompanyModel
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}
