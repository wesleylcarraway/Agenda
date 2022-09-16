using Agenda.Domain.Core;

namespace Agenda.Domain.Models
{
    public class Contact : Register
    {
        public User User { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public IEnumerable<Phone> Phones { get; set; }
    }
}
