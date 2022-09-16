using Agenda.Domain.Interfaces;

namespace Agenda.Domain.Models.Auth
{
    public class AuthUser : IAuthUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
