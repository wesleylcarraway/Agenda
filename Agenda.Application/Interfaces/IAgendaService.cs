using Agenda.Application.Params;
using Agenda.Application.ViewModels.Contact;
using Agenda.Application.ViewModels.PhoneType;

namespace Agenda.Application.Interfaces
{
    public interface IAgendaService
    {
        Task<IEnumerable<ContactResponse>> GetAsync(ContactParams queryParams = null);
        Task<ContactResponse> GetByIdAsync(int id);
        Task<ContactResponse> AddAsync(ContactRequest contactRequest);
        Task<ContactResponse> UpdateAsync(ContactRequest contactRequest, int id);
        Task<ContactResponse> RemoveAsync(int id);
        Task<int> CountAsync(ContactParams queryParams);
        IEnumerable<PhoneTypeResponse> GetPhoneTypes();
        bool PhoneNumberExists(string number);
    }
}
