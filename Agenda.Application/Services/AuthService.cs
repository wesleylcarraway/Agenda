using System.Security.Claims;
using Agenda.Application.Interfaces;
using Agenda.Domain.Models.Auth;
using Microsoft.AspNetCore.Http;

namespace Agenda.Application.Services
{
    public class AuthService : IAuthService
    {
        public AuthUser AuthUser { get; private set; }

        public AuthService(IHttpContextAccessor httpContextAccessor = null)
        {
            if (httpContextAccessor != null)
                SetLoggedUser(httpContextAccessor.HttpContext.User);
        }

        public void SetLoggedUser(ClaimsPrincipal claims)
        {
            if (claims.Identity.IsAuthenticated)
            {
                AuthUser = new AuthUser
                {
                    Id = int.Parse(claims.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value),
                    Name = claims.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value,
                    Email = claims.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value,
                };
            }
        }
    }
}
