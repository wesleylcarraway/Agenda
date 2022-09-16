using AgendaConsole.Models;
using AgendaConsole.JsonStorage;

namespace AgendaConsole.Helpers
{
    public class ContactValidations
    {
        private readonly JsonStorage<Contact> _jsonStorage;
        public ContactValidations(JsonStorage<Contact> jsonStorage)
        {
            _jsonStorage = jsonStorage;
        }
        public bool ContactExists(int id)
        {
            return _jsonStorage.GetAll().Any(x => x.Id == id);
        }

        public bool PhoneExists(int id)
        {
            return _jsonStorage.GetAll().Any(contact => contact.Phones.Any(p => p.Id == id));
        }

        public bool PhoneNumberExists(string number)
        {
            return _jsonStorage.GetAll().Any(contact => contact.Phones.Any(p => p.FormattedPhone == number));
        }
    }
}
