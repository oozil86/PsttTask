using Microsoft.AspNetCore.Identity;

namespace PsttTask.Domain.Contracts
{
    public interface IJWTTokenGenerator
    {
        string GenerateToken(IdentityUser user);
    }
}
