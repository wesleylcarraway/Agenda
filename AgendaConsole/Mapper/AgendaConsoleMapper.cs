using AgendaConsole.Models;
using AgendaConsole.Models.Dtos;

namespace AgendaConsole.Mapper
{
    public class AgendaConsoleMapper
    {
        public Contact MapContact(Contact contact, ContactDto contactDto)
        {
            contact.Name = contactDto.Name;
            contact.Phones = new List<Phone>();
            return contact;
        }

        public Contact MapContactUpdate(Contact contact, ContactUpdateDto contactUpdateDto)
        {
            contact.Id = contactUpdateDto.Id;
            contact.Name = contactUpdateDto.Name;
            return contact;
        }

        public Phone MapPhone(Phone phone, PhoneDto phoneDto)
        {
            phone.ContactId = phoneDto.ContactId;
            phone.Description = phoneDto.Description;
            phone.FormattedPhone = phoneDto.FormattedPhone;
            phone.Ddd = phoneDto.Ddd;
            phone.Number = phoneDto.Number;
            return phone;
        }

        public Phone MapPhoneUpdate(Phone phone, PhoneUpdateDto phoneUpdateDto)
        {
            phone.Id = phoneUpdateDto.Id;
            phone.ContactId = phoneUpdateDto.ContactId;
            phone.Description = phoneUpdateDto.Description;
            phone.FormattedPhone = phoneUpdateDto.FormattedPhone;
            phone.Ddd = phoneUpdateDto.Ddd;
            phone.Number = phoneUpdateDto.Number;
            return phone;
        }
    }
}
