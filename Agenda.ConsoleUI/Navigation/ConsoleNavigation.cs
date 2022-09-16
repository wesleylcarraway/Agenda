namespace Agenda.ConsoleUI.Navigation
{
    public class ConsoleNavigation
    {
        public Dictionary<string, Action<int?>> Navigate { get; set; } = new Dictionary<string, Action<int?>>();
    }
}
