using Agenda.Domain.Core;

namespace Agenda.Domain.Models.Enumerations
{
    public class UserRole : Enumeration
    {
        public static UserRole Admin = new UserRole(1, nameof(Admin));
        public static UserRole Common = new UserRole(2, nameof(Common));

        public UserRole(int id, string name) : base(id, name)
        { }
    }
}
