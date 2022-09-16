using System.Text.RegularExpressions;

namespace AgendaConsole.Helpers
{
    public class InputValidations
    {
        private readonly ConsoleInput _consoleInput;
        private readonly ContactValidations _contactValidations;

        public InputValidations(ConsoleInput consoleInput, ContactValidations contactValidations)
        {
            _consoleInput = consoleInput;
            _contactValidations = contactValidations;
        }

        public int ValidateNumberInputContact(ref int id)
        {
            while(!_contactValidations.ContactExists(id))
            {
                id = _consoleInput.ReadNumber("Enter a value that exists!");
            }
            return id;
        }
        public int ValidateNumberInputPhone(ref int id)
        {
            while(!_contactValidations.PhoneExists(id))
            {
                id = _consoleInput.ReadNumber("Enter a value that exists!");
            }
            return id;
        }

        public string CheckIfNumberExists(ref string number)
        {
            while(_contactValidations.PhoneNumberExists(number))
            {
                number = _consoleInput.ReadString("That number already exists! please enter another: ");
            }
            return number;
        }

        public string ValidateFormattedNumber(ref string formattedPhone)
        {
            while(!IsRegexValid(formattedPhone))
            {
                formattedPhone = _consoleInput.ReadString("Please enter a valid value: (xx) x?xxxx-xxxx");
            }
            return formattedPhone;
        }

        private bool IsRegexValid(string number)
        {
            return new Regex(@"^\(?[1-9]{2}\)? ?(?:[2-8]|9[1-9])[0-9]{3}\-?[0-9]{4}$").IsMatch(number);
        }
    }
}
