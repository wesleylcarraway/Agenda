using Agenda.Application.Services.Interfaces;
using Agenda.ConsoleUI.Navigation;
using AutoMapper;
using Agenda.Application.Params;
using Agenda.ConsoleUI.Utils;
using Agenda.Application.Dtos;

namespace Agenda.ConsoleUI.Views
{
    public class ListSchedule : ConsoleBase
    {
        private readonly IAgendaService _agendaService;
        private readonly IMapper _mapper;
        public ListSchedule
        (
            IAgendaService agendaService,
            ConsoleNavigation consoleNavigation,
            IMapper mapper
        ) : base(consoleNavigation)
        {
            _agendaService = agendaService;
            _mapper = mapper;
        }

        public async void Render()
        {
            Console.Clear();
            await RenderContacts();

            Console.WriteLine("- SEARCH CONTACTS -");
            Console.WriteLine("1 - Search by name\n" +
            "2 - Search by ddd\n" +
            "3 - Search by number\n" +
            "4 - BACK\n"
            );

            int option = ConsoleInput.ReadNumber("<- Enter an option ->");

            var query = new ContactParams();
            switch (option)
            {
                case 1:
                    query.Name = ConsoleInput.ReadString("Enter contact name: ");
                    break;
                case 2:
                    query.DDD = ConsoleInput.ReadNumber("Enter phone DDD: ");
                    break;
                case 3:
                    query.Number = ConsoleInput.ReadString("Enter phone number: ");
                    break;
                case 4:
                    NavigateTo("HomeView");
                    return;
            }
            SearchContacts(query);
        }

        public async Task RenderContacts()
        {
            Console.Clear();
            Console.WriteLine("- SCHEDULE -");
            foreach (ContactDto contact in await _agendaService.GetAsync(new ContactParams()))
                Console.WriteLine(contact);

            Console.WriteLine();
        }

        private async void SearchContacts(ContactParams query)
        {
            Console.Clear();

            var contacts = (await _agendaService.GetAsync(query)).ToList();

            Console.WriteLine("- SEARCH || CONTACTS:");
            foreach (var contact in contacts)
                Console.WriteLine(contact);

            Console.Write("press any key to continue...");
            Console.ReadKey();
            Render();
        }
    }
}
