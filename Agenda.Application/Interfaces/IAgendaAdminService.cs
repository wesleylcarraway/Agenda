using Agenda.Application.Params;
using Agenda.Application.ViewModels.Contact;

namespace Agenda.Application.Interfaces
{
    public interface IAgendaAdminService
    {
        Task<IEnumerable<AdminContactResponse>> GetAsync(AdminContactParams queryParams = null);
        Task<AdminContactResponse> GetByIdAsync(int id);
        Task<AdminContactResponse> AddAsync(AdminContactRequest adminContactRequest);
        Task<AdminContactResponse> UpdateAsync(AdminContactRequest adminContactRequest, int id);
        Task<AdminContactResponse> RemoveAsync(int id);
        bool PhoneNumberExists(string number);
    }
}
