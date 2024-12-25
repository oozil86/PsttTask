using PsttTask.Domain.Contracts;

namespace PsttTask.Domain.Entities;

public class Company : AggregateRoot<long>
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Phone { get; private set; }
    public Branch Branch { get; private set; }


    private Company() { }

    public Company(string name, string email, string phone)
    {
        UpdateName(name);
        UpdateEmail(email);
        UpdatePhone(phone);
    }

    public string GetName() => Name;
    public void UpdateName(string name)
    {
        Name = name;
    }
    public void UpdateEmail(string email)
    {
        Email = email;
    }
    public void UpdatePhone(string phone)
    {
        Phone = phone;
    }
    public void Update(string name, string email, string phone)
    {
        UpdateName(name);
        UpdateEmail(email);
        UpdatePhone(phone);
    }

}
