using AgendaConsole.Models;
using AgendaConsole.JsonStorage;
using AgendaConsole.Repository;

namespace AgendaConsole.Controllers
{
    public class PhoneController
    {
        private readonly PhoneRepository _phoneRepository;
        private readonly JsonStorage<Contact> _jsonStorage;
        public PhoneController(JsonStorage<Contact> jsonStorage, PhoneRepository phoneRepository)
        {
            _jsonStorage = jsonStorage;
            _phoneRepository = phoneRepository;
        }

        public void Add(Phone phone)
        {
            phone.Id = PhoneLastId() + 1;

            _phoneRepository.Add(phone);
        }

        public void Update(Phone phone)
        {
            _phoneRepository.Update(phone);
        }

        public void Remove(int id)
        {
            _phoneRepository.Remove(id);
        }

        private int PhoneLastId()
        {
            int count = 0;
            foreach(var item in _jsonStorage.GetAll())
            {
                count += item.Phones.Count();
            }
            return count;
        }
    }
}
