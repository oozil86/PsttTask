using Microsoft.AspNetCore.Http;
using PsttTask.Domain.Data;
using PsttTask.Domain.Enums;
using System.Security.Claims;

namespace PsttTask.Infrastructure.Data
{
    public class UserIdentity : IUserIdentity
    {
        private static ClaimsPrincipal _user;
        private static ClaimsIdentity _claims;

        public UserIdentity(
            IHttpContextAccessor httpContextAccessor = null
            )
        {
            if (httpContextAccessor != null)
            {
                if (httpContextAccessor.HttpContext.User != null)
                {
                    _user = httpContextAccessor.HttpContext.User;
                    _claims = (ClaimsIdentity)User?.Identity;
                }
            }
        }

        public ClaimsPrincipal User => _user;
        public bool? IsAuthenticated => _user?.Identity?.IsAuthenticated;
        public string UserId => GetClaims(ClaimTypes.Sid);
        public string Email => GetClaims(ClaimTypes.Email);
        public string Phone => GetClaims(ClaimTypes.MobilePhone);
        public string UserName => GetUserName();
        public string Name => GetClaims(ClaimTypes.GivenName) ?? "Anonymous";
        public string NormalizedName => GetClaims(ClaimTypes.GivenName);
        public EnumUserCategory? UserCategory => GetUserCategory();

        EnumUserCategory? IUserIdentity.UserCategory => throw new NotImplementedException();

        public bool IsInRole(string roleName)
        {
            return _user.IsInRole(roleName);
        }

        private static string GetUserName()
        {
            var IsAuthenticated = _user?.Identity?.IsAuthenticated;
            if (IsAuthenticated != null)
            {
                if ((bool)IsAuthenticated)
                {
                    var claims = _claims.Claims.ToList();
                    return string.IsNullOrEmpty(claims[0].Value) ? null : claims[0].Value;
                }
            }
            return null;
        }

        private static EnumUserCategory? GetUserCategory()
        {
            var claim = GetClaims(ClaimTypes.AuthorizationDecision);
            if (string.IsNullOrEmpty(claim))
                return null;
            var userCategory = Enum.Parse(typeof(EnumUserCategory), claim);
            return userCategory as EnumUserCategory?;
        }

        private static string GetClaims(string claimName)
        {
            return _claims?.FindFirst(claimName)?.Value;
        }
    }
}
