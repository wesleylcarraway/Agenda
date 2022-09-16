using System.Security.Claims;

namespace Agenda.Application.Interfaces
{
    public interface IUser
    {
        string Name { get; }
        int GetUserId();
        string GetUserEmail();
        bool IsAuthenticated();
        bool IsInRole(string role);
        IEnumerable<Claim> GetClaimsIdentity();
    }
}
