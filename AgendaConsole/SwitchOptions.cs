using AgendaConsole.Helpers;
using AgendaConsole.Controllers;
using AgendaConsole.Views;

namespace AgendaConsole
{
    public class SwitchOptions
    {
        MainView mainView = new MainView();
        private readonly ConsoleInput _consoleInput;
        private readonly OptionsController _optionsController;
        private readonly AgendaView _agendaView;

        public SwitchOptions(ConsoleInput consoleInput, OptionsController optionsController, AgendaView agendaView)
        {
            _consoleInput = consoleInput;
            _optionsController = optionsController;
            _agendaView = agendaView;
        }

        public void Switch()
        {
            do
            {
                var option = _consoleInput.ReadNumber("-> Enter one of the options <-");

                if (option == 0)
                {
                    Console.Beep();
                    return;
                }

                Dictionary<int, Action> options = new Dictionary<int, Action>();

                options.Add(1, _optionsController.CreateContact);
                options.Add(2, _optionsController.EditContact);
                options.Add(3, _optionsController.DeleteContact);
                options.Add(4, _optionsController.CreatePhone);
                options.Add(5, _optionsController.EditPhone);
                options.Add(6, _optionsController.DeletePhone);
                options.Add(7, _optionsController.SearchContactsbyName);
                options.Add(8, _optionsController.SearchPhonesByDdd);
                options.Add(9, _optionsController.SearchPhonesByNumber);
                options.Add(10, _agendaView.Contacts);
                options.Add(11, _optionsController.SaveAll);

                if (options.ContainsKey(option))
                {
                    options[option].Invoke();
                        mainView.MainScreen();
                }else
                    Console.WriteLine("Enter a valid option!");

            }
            while(true);
        }
    }
}
