using AgendaConsole.Models;
using AgendaConsole.JsonStorage;
using AgendaConsole.Views.ContactViews;
using AgendaConsole.Views;
using AgendaConsole.Mapper;

namespace AgendaConsole.Controllers
{
    public class OptionsController
    {
        private readonly JsonStorage<Contact> _jsonStorage;
        private readonly ContactController _contactController;
        private readonly AgendaConsoleMapper _mapper;
        private readonly ContactViews _contactViews;
        private readonly AgendaView _agendaView;
        private readonly ErrorView _errorView;
        private readonly PhoneViews _phoneViews;
        private readonly PhoneController _phoneController;

        public OptionsController(
            JsonStorage<Contact> jsonStorage,
            ContactController contactController,
            AgendaConsoleMapper mapper,
            ContactViews contactViews,
            AgendaView agendaView,
            ErrorView errorView,
            PhoneViews phoneViews,
            PhoneController phoneController
            )
        {
            _jsonStorage = jsonStorage;
            _contactController = contactController;
            _mapper = mapper;
            _contactViews = contactViews;
            _agendaView = agendaView;
            _errorView = errorView;
            _phoneViews = phoneViews;
            _phoneController = phoneController;
        }

        public void CreateContact()
        {
            var contact = _mapper.MapContact(new Contact(), _contactViews.Create());
            _contactController.Add(contact);
            _agendaView.Contacts();
        }

        public void EditContact()
        {
            if(_jsonStorage.GetAll().Any())
            {
                _agendaView.Contacts();
                var contact = _mapper.MapContactUpdate(new Contact(), _contactViews.Edit());
                _contactController.Update(contact);
            }
            else _errorView.NoContacts();
        }

        public void DeleteContact()
        {
            if(_jsonStorage.GetAll().Any())
            {
                _agendaView.Contacts();
                _contactController.Remove(_contactViews.Delete());
            }
            else _errorView.NoContacts();
        }

        public void CreatePhone()
        {
            if(_jsonStorage.GetAll().Any())
            {
                var phone = _mapper.MapPhone(new Phone(), _phoneViews.Create());
                _phoneController.Add(phone);
                _agendaView.Contacts();
            }
            else _errorView.NoContacts();
        }

        public void EditPhone()
        {
            if(_jsonStorage.GetAll().Any(x => x.Phones.Any()))
            {
                _agendaView.Contacts();
                var phone = _mapper.MapPhoneUpdate(new Phone(), _phoneViews.Edit());
                _phoneController.Update(phone);
            }
            else _errorView.NoPhones();
        }

        public void DeletePhone()
        {
            if(_jsonStorage.GetAll().Any(x => x.Phones.Any()))
            {
                _agendaView.Contacts();
                _phoneController.Remove(_phoneViews.Delete());
            }
            else _errorView.NoPhones();
        }

        public void SearchContactsbyName()
        {
            var contacts = _contactController.GetAll();
            _agendaView.ContactsByName(contacts, _contactViews.GetByName());
        }

        public void SearchPhonesByDdd()
        {
            var contacts = _contactController.GetAll();
            _agendaView.ContactsByDdd(contacts, _phoneViews.GetAllByDdd());
        }

        public void SearchPhonesByNumber()
        {
            var contacts = _contactController.GetAll();
            _agendaView.ContactsByNumber(contacts, _phoneViews.GetAllByNumber());
        }

        public async void SaveAll()
        {
            await _jsonStorage.SaveAsync();
        }
    }
}
