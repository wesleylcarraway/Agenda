using AgendaConsole.Models.Dtos;
using AgendaConsole.Models;
using AgendaConsole.JsonStorage;
using AgendaConsole.Helpers;

namespace AgendaConsole.Views
{
    public class PhoneViews
    {
        private readonly ConsoleInput _consoleInput;
        private readonly InputValidations _validations;
        public PhoneViews(ConsoleInput consoleInput, InputValidations validations)
        {
            _consoleInput = consoleInput;
            _validations = validations;
        }
        public PhoneDto Create()
        {
            int contactId = _consoleInput.ReadNumber("Which contact do you want to create the phone for? ");

            _validations.ValidateNumberInputContact(ref contactId);

            string description = _consoleInput.ReadString("Enter phone description: ");

            string formattedPhone = _consoleInput.ReadString("Enter phone number: ");

            _validations.CheckIfNumberExists(ref formattedPhone);

            _validations.ValidateFormattedNumber(ref formattedPhone);

            return new PhoneDto(contactId, description, formattedPhone);
        }

        public PhoneUpdateDto Edit()
        {
            int contactId = _consoleInput.ReadNumber("Which contact do you want to edit the phone for? ");

            _validations.ValidateNumberInputContact(ref contactId);

            int id = _consoleInput.ReadNumber("Which phone do you want to update? ");

            _validations.ValidateNumberInputPhone(ref id);

            string description = _consoleInput.ReadString("Enter the new phone description: ");

            string formattedPhone = _consoleInput.ReadString("Enter the new phone number: ");

            _validations.CheckIfNumberExists(ref formattedPhone);

            _validations.ValidateFormattedNumber(ref formattedPhone);

            return new PhoneUpdateDto(contactId ,id, description, formattedPhone);
        }

        public int Delete()
        {
            int id = _consoleInput.ReadNumber("Which phone do you want to delete? ");

            _validations.ValidateNumberInputPhone(ref id);

            return id;
        }

        public int GetAllByDdd()
        {
            return _consoleInput.ReadNumber("Enter DDD: ");
        }

        public string GetAllByNumber()
        {
            return _consoleInput.ReadString("Enter number: ");
        }
    }
}
