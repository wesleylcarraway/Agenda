using Agenda.Application.ViewModels.Email;

namespace Agenda.Application.Interfaces
{
    public interface ISendEmailService
    {
        Task<EmailResponse> Send(EmailRequest EmailRequest);
    }
}