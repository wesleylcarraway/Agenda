using Agenda.Application.Dtos;
using Agenda.ConsoleUI.Views.Common;
using AutoMapper;
using Agenda.Application.Services.Interfaces;
using Agenda.ConsoleUI.Navigation;
using Agenda.ConsoleUI.Utils;

namespace Agenda.ConsoleUI.Views
{
    public class EditContact : ConsoleBase
    {
        private readonly IMapper _mapper;
        private readonly IAgendaService _agendaService;
        private readonly ListSchedule _listSchedule;
        private ContactDto _contact = new ContactDto();
        private int _contactId;

        public EditContact
        (
            IMapper mapper,
            IAgendaService agendaService,
            ListSchedule listSchedule,
            ConsoleNavigation consoleNavigation
        ) : base(consoleNavigation)
        {
            _mapper = mapper;
            _agendaService = agendaService;
            _listSchedule = listSchedule;
        }

        public async void RenderAsync()
        {
            await _listSchedule.RenderContacts();

            _contactId = await RenderGetContactId();
            _contact = await _agendaService.GetByIdAsync(_contactId);

            Render();
        }
        private void Render()
        {
            Dictionary<int, Action> options = new Dictionary<int, Action>()
            {
                {1, () => UpdateContact()},
                {2, () => RemoveContact()},
                {3, () => AddPhone()},
                {4, () => UpdatePhone()},
                {5, () => RemovePhoneAsync()}
            };

            Console.Clear();
            Console.WriteLine("- EDIT CONTACTS -");
            Console.WriteLine("1 - Edit contact's name\n" +
            "2 - Remove Contact\n" +
            "\n" +
            "3 - Add a phone\n" +
            "4 - Edit a phone\n" +
            "5 - Remove a phone"
            );

            int option = ConsoleInput.ReadNumber("<- Enter an option ->");

            options[option]();

            NavigateTo("HomeView");
        }

        private void UpdateContact()
        {
            Console.Clear();
            _contact.Name = ContactInput.ReadName();
            Save();
            LoadingView.Loading("Contact updated with success! ");
        }

        private async Task RemoveContact()
        {
            Console.Clear();
            await _listSchedule.RenderContacts();
            Console.WriteLine($"Contact with Id: {_contactId} will be removed...");

            if(ConsoleInput.ReadString("Are you sure you want to remove it? (y/n) ") == "y")
            {
                try
                {
                    await _agendaService.RemoveAsync(_contactId);
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                LoadingView.Loading("Contact removed with success! ");
            }
        }

        private async Task RemovePhoneAsync()
        {
            Console.Clear();
            foreach(PhoneDto p in _contact.Phones)
            {
                Console.WriteLine(p);
            }

            int id = await RenderGetPhoneId();

            if(ConsoleInput.ReadString("Are you sure you want to remove it? (y/n) ") == "y")
            {
                PhoneDto phone = _contact.Phones.FirstOrDefault(x => x.Id == id);
                List<PhoneDto> phones = _contact.Phones.ToList();

                phones.Remove(phone);
                _contact.Phones = phones;

                Save();
                LoadingView.Loading("Phone removed with success!");
            }
        }

        private void AddPhone()
        {
            string number = PhoneInput.ReadNumber();
            PhoneInput.ValidateFormattedNumber(ref number);

            while(_agendaService.PhoneNumberExists(number))
            {
                number = ConsoleInput.ReadString("That number already exists! please enter another: ");
            }

            string description = PhoneInput.ReadDescription();

            PhoneDto phoneDto = new PhoneDto(number, description, AddPhoneType(), DateTime.Now);
            List<PhoneDto> phones = _contact.Phones.ToList();
            phones.Add(phoneDto);
            _contact.Phones = phones;

            Save();
            LoadingView.Loading("Phone added with success!");
        }

        private int AddPhoneType()
        {
            Console.WriteLine("1 - Residencial\n" +
            "2 - Cellphone\n" +
            "3 - Commercial\n");
            int phoneTypeId = ConsoleInput.ReadNumber("Enter phone type: ");
            PhoneTypeInput.ValidatePhoneTypeId(ref phoneTypeId);
            return phoneTypeId;
        }


        private async Task UpdatePhone()
        {
            Console.Clear();
            foreach(PhoneDto p in _contact.Phones)
            {
                Console.WriteLine(p);
            }

            int id = await RenderGetPhoneId();

            string number = PhoneInput.ReadNumber();
            PhoneInput.ValidateFormattedNumber(ref number);

            while(_agendaService.PhoneNumberExists(number))
            {
                number = ConsoleInput.ReadString("That number already exists! please enter another: ");
            }

            string description = PhoneInput.ReadDescription();

            List<PhoneDto> phones = _contact.Phones.ToList();

            PhoneDto phone = phones.FirstOrDefault(x => x.Id == id);
            int index = phones.IndexOf(phone);

            phone.UpdatedAt = DateTime.Now;
            phone.FormattedNumber = number;
            phone.Description = description;
            phone.PhoneTypeId = AddPhoneType();

            phones[index] = phone;
            _contact.Phones = phones;

            Save();
            LoadingView.Loading("Phone updated with success! ");
        }

        private async Task<int> RenderGetPhoneId()
        {
            int id = ConsoleInput.ReadNumber("Enter phone id: ");

            if (!_contact.Phones.Any(x => x.Id == id))
            {
                Thread.Sleep(1000);
                Console.WriteLine("Id not found, please try again...");
                id = await RenderGetPhoneId();
            }
            return id;
        }

        private async Task<int> RenderGetContactId()
        {
            int id = ConsoleInput.ReadNumber("Which contact do you want to edit? ");
            ContactDto contact = await _agendaService.GetByIdAsync(id);

            if (contact == null)
            {
                Thread.Sleep(1000);
                Console.WriteLine("Id not found, please try again...");
                id = await RenderGetContactId();
            }
            return id;
        }

        private void Save()
        {
            try{
                _agendaService.UpdateAsync(_contact, _contact.Id);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
