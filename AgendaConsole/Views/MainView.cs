namespace AgendaConsole.Views
{
    public class MainView
    {
        public void WelcomeScreen()
        {
            Console.WriteLine("WELCOME TO YOUR CONTACT SCHEDULE!");
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
        }
        public void MainScreen()
        {
            Console.WriteLine("-CONTACTS SESSION-");
            Console.WriteLine("1 - Create contact");
            Console.WriteLine("2 - Edit contact");
            Console.WriteLine("3 - delete contact");
            Console.WriteLine();
            Console.WriteLine("-PHONES SESSION-");
            Console.WriteLine("4 - Create phone");
            Console.WriteLine("5 - Edit phone");
            Console.WriteLine("6 - delete phone");
            Console.WriteLine();
            Console.WriteLine("-PESQUISAR-");
            Console.WriteLine("7 - Search contacts by name");
            Console.WriteLine("8 - Search phones by DDD");
            Console.WriteLine("9 - Search phones by number");
            Console.WriteLine();
            Console.WriteLine("10 - List schedule");
            Console.WriteLine("11 - Save all");
            Console.WriteLine("0 - EXIT");
            Console.WriteLine();
        }
    }
}
