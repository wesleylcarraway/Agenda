using Agenda.Domain.Core;
using Agenda.Domain.Models.Enumerations;

namespace Agenda.ConsoleUI.Utils
{
    public class PhoneTypeInput
    {
        static public int ValidatePhoneTypeId(ref int id)
        {
            int auxNumber = id;
            List<PhoneType> idList = Enumeration.GetAll<PhoneType>().ToList();
            while(idList.TrueForAll(x => x.Id != auxNumber))
            {
                auxNumber = ConsoleInput.ReadNumber("Enter a valid option! ");
            }
            id = auxNumber;
            return id;
        }
    }
}
