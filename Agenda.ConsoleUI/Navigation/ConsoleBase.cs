namespace Agenda.ConsoleUI.Navigation
{
    public class ConsoleBase
    {
        private readonly ConsoleNavigation _consoleNavigation;

        public ConsoleBase(ConsoleNavigation consoleNavigation)
        {
            _consoleNavigation = consoleNavigation;
        }

        protected void NavigateTo(string route)
        {
            _consoleNavigation.Navigate[route](null);
        }
    }
}
