using AgendaConsole.Models;
using AgendaConsole.JsonStorage;

namespace AgendaConsole.Controllers
{
    public class ContactController
    {
        private readonly JsonStorage<Contact> _jsonStorage;
        public ContactController(JsonStorage<Contact> jsonStorage)
        {
            _jsonStorage = jsonStorage;
        }

        public void Add(Contact contact)
        {
            _jsonStorage.Create(contact);
        }

        public void Update(Contact contact)
        {
            var contactInStorage = _jsonStorage.GetById(contact.Id);
            contact.Phones = contactInStorage.Phones;
            _jsonStorage.Update(contact);
        }

        public void Remove(int id)
        {
            _jsonStorage.Remove(id);
        }

        public List<Contact> GetAll()
        {
            return _jsonStorage.GetAll().ToList();
        }
    }
}
