using AgendaConsole.JsonStorage;
using AgendaConsole.Models;

namespace AgendaConsole.Views
{
    public class AgendaView
    {
        private readonly JsonStorage<Contact> _jsonStorage;

        public AgendaView(JsonStorage<Contact> jsonStorage)
        {
            _jsonStorage = jsonStorage;
        }
        public void Contacts()
        {
            Console.WriteLine();
            Console.WriteLine("- AGENDA -");
            foreach(var contact in _jsonStorage.GetAll())
            {
                Console.WriteLine(contact);
            }
        }

        public void ContactsByName(List<Contact> contacts, string name)
        {
            Console.WriteLine();
            Console.WriteLine("- CONTACTS -");
            foreach(var contact in contacts.Where(x => x.Name.Contains(name)))
            {
                Console.WriteLine(contact);
            }
        }

        public void ContactsByDdd(List<Contact> contacts, int ddd)
        {
            Console.WriteLine();
            Console.WriteLine("- CONTACTS -");
            foreach(var contact in contacts.Where(c => c.Phones.Any(p => p.Ddd == ddd)))
            {
                Console.WriteLine(contact);
            }
        }

        public void ContactsByNumber(List<Contact> contacts, string number)
        {
            Console.WriteLine();
            Console.WriteLine("- CONTACTS -");
            foreach(var contact in contacts.Where(c => c.Phones.Any(p => p.Number == number)))
            {
                Console.WriteLine(contact);
            }
        }
    }
}
