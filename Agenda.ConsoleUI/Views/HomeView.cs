using Agenda.ConsoleUI.Navigation;
using Agenda.Application.Services.Interfaces;
using Agenda.Application.Params;
using Agenda.ConsoleUI.Utils;
using Agenda.ConsoleUI.Views.Common;

namespace Agenda.ConsoleUI.Views
{
    public class HomeView : ConsoleBase
    {
        private readonly IAgendaService _agendaService;
        private readonly IInteractionService _interactionService;
        private Action _exitAction;

        public HomeView
        (
            ConsoleNavigation consoleNavigation,
            IAgendaService agendaService,
            IInteractionService interactionService
        ) : base(consoleNavigation)
        {
            _agendaService = agendaService;
            _interactionService = interactionService;
        }

        public async Task MainScreen()
        {
            var options = new Dictionary<int, Action>()
            {
                {1, () => NavigateTo("CreateContact")},
                {2, () => NavigateTo("EditContact")},
                {3, () => NavigateTo("ListSchedule")},
                {4, () => SaveInteractions()},
                {0, Exit}
            };

            Console.Clear();
            Console.WriteLine("-MANAGE CONTACTS- \n" +
            "1 - Create a new contact\n" +
            "2 - Edit a contact\n" +
            "3 - List schedule\n" +
            "4 - Save interactions\n" +
            "0 - EXIT\n");

            int option = ConsoleInput.ReadNumber("<- Enter an option ->");

            if(option == 2 && (await _agendaService.GetAsync(new ContactParams())).ToList().Count() < 1)
            {
                Console.WriteLine("There's no contacts yet!");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                NavigateTo("HomeView");
            }

            if (options.ContainsKey(option))
            {
                options[option].Invoke();
            }else
            {
                await MainScreen();
            }

        }

        private async void SaveInteractions()
        {
            try
            {
                _interactionService.SaveJsonInteractionsAsync();
                LoadingView.Loading("Interactions saved! ");
                await MainScreen();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private void Exit()
        {
            if (_exitAction != null)
            {
                _exitAction();
            }
        }

        public HomeView SetExitAction(Action exitAction)
        {
            _exitAction = exitAction;
            return this;
        }
    }
}
