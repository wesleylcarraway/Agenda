using Agenda.Application.ViewModels.PhoneType;
using Agenda.Domain.Core;

namespace Agenda.Application.ViewModels.Phone
{
    public class PhoneResponse : Register
    {
        public PhoneTypeResponse PhoneType { get; set; }
        public int PhoneTypeId {get; set;}
        public string FormattedNumber { get; set; }
        public int DDD { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }
    }
}
