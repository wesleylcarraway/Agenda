using Agenda.Application.Services.Interfaces;
using Agenda.ConsoleUI.Views;
using Microsoft.Extensions.Hosting;

namespace Agenda.ConsoleUI.Navigation
{
    public class ConsoleMain
    {
        private readonly IAgendaService _agendaService;
        private readonly CreateContact _createContact;
        private readonly EditContact _editContact;
        private readonly HomeView _homeView;
        private readonly ConsoleNavigation _consoleNavigation;
        private readonly ListSchedule _listSchedule;

        public ConsoleMain
        (
            IAgendaService agendaService,
            CreateContact createContact,
            EditContact editContact,
            HomeView homeView,
            ConsoleNavigation consoleNavigation,
            ListSchedule listSchedule,
            IHostApplicationLifetime appLifetime
        )
        {
            _homeView = homeView.SetExitAction(() => appLifetime.StopApplication());
            _agendaService = agendaService;
            _createContact = createContact;
            _editContact = editContact;
            _homeView = homeView;
            _consoleNavigation = consoleNavigation;
            _listSchedule = listSchedule;
        }
        public void Start()
        {
            Console.Clear();
            _consoleNavigation.Navigate.Add("CreateContact", _ => _createContact.Render());
            _consoleNavigation.Navigate.Add("EditContact", _ => _editContact.RenderAsync());
            _consoleNavigation.Navigate.Add("HomeView", _ => _homeView.MainScreen());
            _consoleNavigation.Navigate.Add("ListSchedule", _ => _listSchedule.Render());
            _consoleNavigation.Navigate["HomeView"](null);
        }
    }
}
