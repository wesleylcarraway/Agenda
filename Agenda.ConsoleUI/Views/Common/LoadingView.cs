namespace Agenda.ConsoleUI.Views.Common
{
    public class LoadingView
    {
        static public void Loading(string message)
        {
            Console.Clear();
            Thread.Sleep(1000);
            Console.Write(".");
            Thread.Sleep(1200);
            Console.Write(" .");
            Thread.Sleep(1400);
            Console.Write(" .");
            Thread.Sleep(1600);
            Console.WriteLine();
            Console.WriteLine(message);
            Thread.Sleep(1700);
            Console.WriteLine();
            Console.Write("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
