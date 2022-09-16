using AgendaConsole.Models.Dtos;
using AgendaConsole.Helpers;

namespace AgendaConsole.Views.ContactViews;
public class ContactViews
{
    private readonly ConsoleInput _consoleInput;
    private readonly InputValidations _validations;
    public ContactViews(ConsoleInput consoleInput, InputValidations validations)
    {
        _consoleInput = consoleInput;
        _validations = validations;
    }

    public ContactDto Create()
    {
        string name = _consoleInput.ReadString("Enter contact name: ");
        return new ContactDto(name);
    }

    public ContactUpdateDto Edit()
    {
        int id = _consoleInput.ReadNumber("Which contact do you want to edit? ");

        _validations.ValidateNumberInputContact(ref id);

        string name = _consoleInput.ReadString("enter new contact name: ");
        return new ContactUpdateDto(id, name);
    }

    public int Delete()
    {
        int id = _consoleInput.ReadNumber("Which contact do you want to delete? ");

        _validations.ValidateNumberInputContact(ref id);

        return id;
    }

    public string GetByName()
    {
        return _consoleInput.ReadString("Enter name: ");
    }
}
