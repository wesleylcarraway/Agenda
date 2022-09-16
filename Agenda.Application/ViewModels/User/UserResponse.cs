using Agenda.Application.ViewModels.UserRole;
using Agenda.Domain.Core;

namespace Agenda.Application.ViewModels.User
{
    public class UserResponse : Register
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public int UserRoleId { get; set; }
        public UserRoleResponse UserRole { get; set; }

    }
}
