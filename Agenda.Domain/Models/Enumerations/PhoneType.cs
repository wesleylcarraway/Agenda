using Agenda.Domain.Core;

namespace Agenda.Domain.Models.Enumerations
{
    public class PhoneType : Enumeration
    {
        public static PhoneType Residencial = new PhoneType(1, nameof(Residencial));
        public static PhoneType Cellphone = new PhoneType(2, nameof(Cellphone));
        public static PhoneType Commercial = new PhoneType(3, nameof(Commercial));

        public PhoneType(int id, string name) : base(id, name)
        { }
    }
}
