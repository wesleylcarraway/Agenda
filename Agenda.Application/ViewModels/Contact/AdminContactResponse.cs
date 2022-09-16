using Agenda.Application.ViewModels.User;

namespace Agenda.Application.ViewModels.Contact
{
    public class AdminContactResponse : ContactResponse
    {
        public UserResponse User { get; set; }
        public int UserId { get; set; }
    }
}
