using System.Security.Claims;
using Agenda.Domain.Models.Auth;

namespace Agenda.Application.Interfaces
{
    public interface IAuthService
    {
        AuthUser AuthUser { get; }
        void SetLoggedUser(ClaimsPrincipal claims);
    }
}
