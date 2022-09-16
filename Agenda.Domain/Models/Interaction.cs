using Agenda.Domain.Core;
using Agenda.Domain.Models.Enumerations;

namespace Agenda.Domain.Models
{
    public class Interaction : Register
    {
        public User User { get; set; }
        public int UserId { get; set; }
        public int InteractionTypeId { get; set; }
        public InteractionType InteractionType { get; set; }
        public string Message { get; set; }

        public Interaction(int userId, int interactionTypeId, string message)
        {
            UserId = userId;
            InteractionTypeId = interactionTypeId;
            Message = message;
        }
    }
}
