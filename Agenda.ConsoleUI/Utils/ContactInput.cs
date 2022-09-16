namespace Agenda.ConsoleUI.Utils
{
    public class ContactInput
    {
        static public string ReadName()
        {
            string name = ConsoleInput.ReadString("Enter contact's name: ");

            while(ConsoleInput.ReadString($"Is the name '{name}' correct? (y/n) ") != "y")
            {
                name = ConsoleInput.ReadString("Enter contact's name: ");
            }
            return name;
        }
    }
}
