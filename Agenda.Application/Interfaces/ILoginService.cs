using System.Security.Claims;
using Agenda.Application.ViewModels.Login;

namespace Agenda.Application.Interfaces
{
    public interface ILoginService
    {
        Task<IEnumerable<Claim>> Login(LoginRequest model);
    }
}
