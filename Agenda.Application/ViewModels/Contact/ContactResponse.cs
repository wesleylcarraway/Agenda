using Agenda.Application.ViewModels.Phone;
using Agenda.Domain.Core;

namespace Agenda.Application.ViewModels.Contact
{
    public class ContactResponse : Register
    {
        public string Name { get; set; }
        public IEnumerable<PhoneResponse> Phones { get; set; } = new List<PhoneResponse>();
    }
}
