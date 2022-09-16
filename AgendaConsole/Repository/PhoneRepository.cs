using AgendaConsole.Models;
using AgendaConsole.JsonStorage;

namespace AgendaConsole.Repository
{
    public class PhoneRepository
    {
        private readonly JsonStorage<Contact> _jsonStorage;
        private List<Phone> _phones = new List<Phone>();
        public PhoneRepository(JsonStorage<Contact> jsonStorage)
        {
            _jsonStorage = jsonStorage;
        }

        public void Add(Phone phone)
        {
            var contact = _jsonStorage.GetById(phone.ContactId);

            _phones.AddRange(contact.Phones);
            _phones.Add(phone);
            contact.Phones = _phones;

            _phones = new List<Phone>();
            contact.UpdatedAt = DateTime.Now;
        }

        public void Remove(int id)
        {
            foreach(var contact in _jsonStorage.GetAll())
            {
                var phone = contact.Phones.FirstOrDefault(x => x.Id == id);

                _phones.AddRange(contact.Phones);
                _phones.Remove(phone);
                contact.Phones = _phones;

                _phones = new List<Phone>();
                contact.UpdatedAt = DateTime.Now;
            }
        }

        public void Update(Phone phone)
        {
            var contact = _jsonStorage.GetById(phone.ContactId);

            _phones.AddRange(contact.Phones);

            var p = _phones.Find(x => x.Id == phone.Id);

            p.Id = phone.Id;
            p.ContactId = phone.ContactId;
            p.Description = phone.Description;
            p.Ddd = phone.Ddd;
            p.Number = phone.Number;
            p.FormattedPhone = phone.FormattedPhone;

            contact.Phones = _phones;
            _phones = new List<Phone>();

            contact.UpdatedAt = DateTime.Now;
        }
    }
}
