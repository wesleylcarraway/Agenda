using Agenda.Application.Dtos;
using Agenda.ConsoleUI.Views.Common;
using AutoMapper;
using Agenda.ConsoleUI.Utils;
using Agenda.Application.Services.Interfaces;
using Agenda.ConsoleUI.Navigation;

namespace Agenda.ConsoleUI.Views
{
    public class CreateContact : ConsoleBase
    {
        private readonly IMapper _mapper;
        private readonly IAgendaService _agendaService;
        private ContactDto _contact = new ContactDto();

        public CreateContact
        (
            ConsoleNavigation consoleNavigation,
            IMapper mapper,
            IAgendaService agendaService
        ) : base(consoleNavigation)
        {
            _mapper = mapper;
            _agendaService = agendaService;
        }

        public async Task Render()
        {
            Console.Clear();
            Console.WriteLine("- CREATE NEW CONTACT -");

            AddContact();

            if(ConsoleInput.ReadString("Wish add a phone? (y/n) ") == "y")
                AddPhone();

            await Save();

            LoadingView.Loading("Contact created with success!");
            NavigateTo("HomeView");
            return;
        }

        private void AddContact()
        {
            _contact.Name = ContactInput.ReadName();
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

        private async Task Save()
        {
            try
            {
                await _agendaService.AddAsync(_contact);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
