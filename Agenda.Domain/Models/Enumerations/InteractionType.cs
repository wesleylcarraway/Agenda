using Agenda.Domain.Core;

namespace Agenda.Domain.Models.Enumerations
{
    public class InteractionType : Enumeration
    {
        public static InteractionType CreateContact = new InteractionType(1, "Create Contact");
        public static InteractionType UpdateContact = new InteractionType(2, "Update Contact");
        public static InteractionType RemoveContact = new InteractionType(3, "Delete Contact");
        public static InteractionType GetContact = new InteractionType(4, "View Contact");
        public static InteractionType GetPhones = new InteractionType(5, "View Phones");

        public InteractionType(int id, string name) : base(id, name)
        {
        }
    }
}
