using AgendaConsole.Models;
using AgendaConsole.Controllers;
using AgendaConsole.JsonStorage;
using AgendaConsole.Views.ContactViews;
using AgendaConsole.Views;
using AgendaConsole.Mapper;
using AgendaConsole.Helpers;
using AgendaConsole.Repository;
using AgendaConsole;


JsonStorage<Contact> jsonStorage = new JsonStorage<Contact>();
PhoneRepository phoneRepository = new PhoneRepository(jsonStorage);
ContactController contactController = new ContactController(jsonStorage);
PhoneController phoneController = new PhoneController(jsonStorage, phoneRepository);
AgendaConsoleMapper mapper = new AgendaConsoleMapper();
ConsoleInput consoleInput = new ConsoleInput();
ContactValidations contactValidations = new ContactValidations(jsonStorage);
InputValidations inputValidations = new InputValidations(consoleInput, contactValidations);
MainView mainView = new MainView();
ContactViews contactViews = new ContactViews(consoleInput, inputValidations);
PhoneViews phoneViews = new PhoneViews(consoleInput, inputValidations);
ErrorView errorView = new ErrorView();
AgendaView agendaView = new AgendaView(jsonStorage);
OptionsController optionsController = new OptionsController(
    jsonStorage,
    contactController,
    mapper,
    contactViews,
    agendaView,
    errorView,
    phoneViews,
    phoneController
    );
SwitchOptions switchOptions = new SwitchOptions(consoleInput, optionsController, agendaView);

void Start()
{
    mainView.WelcomeScreen();
    Main();
}

void Main()
{
    mainView.MainScreen();
    switchOptions.Switch();
}

Start();
