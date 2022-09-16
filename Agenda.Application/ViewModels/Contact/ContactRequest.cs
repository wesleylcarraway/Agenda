using Agenda.Application.ViewModels.Phone;

namespace Agenda.Application.ViewModels.Contact
{
    public class ContactRequest
    {
        public string Name { get; set; }
        public IEnumerable<PhoneRequest> Phones { get; set; }
    }
}
