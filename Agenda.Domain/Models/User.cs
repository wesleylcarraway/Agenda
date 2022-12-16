using Agenda.Domain.Core;
using Agenda.Domain.Models.Enumerations;

namespace Agenda.Domain.Models
{
    public class User : Register
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRole UserRole { get; set; }
        public int UserRoleId { get; set; }

    }
}
