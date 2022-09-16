using Agenda.Domain.Core;
using Agenda.Domain.Models.Enumerations;

namespace Agenda.Domain.Models
{
    public class Phone : Register
    {
        public string Number { get; set; }
        public int DDD { get; set; }
        public string FormattedNumber { get; set; }
        public string Description { get; set; }
        public int ContactId { get; set; }
        public Contact Contact { get; set; }
        public int PhoneTypeId { get; set; }
        public PhoneType PhoneType { get; set;}

    }
}
