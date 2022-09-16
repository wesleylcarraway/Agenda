using System.Text.RegularExpressions;

namespace Agenda.Application.Utils
{
    public class ValidatePhoneNumber
    {
        public static bool IsValid(string number)
        {
            return new Regex(@"^\(?[1-9]{2}\)? ?(?:[2-8]|9[1-9])[0-9]{3}\-?[0-9]{4}$").IsMatch(number);
        }
    }
}
