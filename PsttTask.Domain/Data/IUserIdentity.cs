using PsttTask.Domain.Enums;
using System.Security.Claims;

namespace PsttTask.Domain.Data;

public interface IUserIdentity
{
    ClaimsPrincipal User { get; }

    bool? IsAuthenticated { get; }

    string UserId { get; }

    string Email { get; }

    string Phone { get; }

    string UserName { get; }

    string Name { get; }

    string NormalizedName { get; }

    EnumUserCategory? UserCategory { get; }

    //public bool IsInRole(string roleName) { }

}