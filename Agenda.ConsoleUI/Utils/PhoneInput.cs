using Agenda.Application.Utils;

namespace Agenda.ConsoleUI.Utils
{
    public class PhoneInput
    {
        static public string ReadNumber()
        {
            string number = ConsoleInput.ReadString("Enter phone number: ");

            while(ConsoleInput.ReadString($"Is the number '{number}' correct? (y/n) ") != "y")
            {
                number = ConsoleInput.ReadString("Enter phone number: ");
            }
            return number;
        }

        static public string ReadDescription()
        {
            string description = ConsoleInput.ReadString("Enter phone description: ");
            while(ConsoleInput.ReadString($"Is the description '{description}' correct? (y/n) ") != "y")
            {
                description = ConsoleInput.ReadString("Enter phone description: ");
            }
            return description;
        }

        static public string ValidateFormattedNumber(ref string formattedPhone)
        {
            while(!FormatPhone.IsRegexValid(formattedPhone))
            {
                formattedPhone = ConsoleInput.ReadString("Please enter a valid value: (xx) x?xxxx-xxxx ");
            }
            return formattedPhone;
        }
    }
}
