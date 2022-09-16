using System.Text.RegularExpressions;

namespace Agenda.Application.Utils
{
    public class FormatPhone
    {
        public static (int, string) Split(string formattedNumber)
        {
            return (int.Parse(formattedNumber.Substring(1, 2)),
                    Regex.Replace(formattedNumber.Substring(4), @"[^\d]", ""));
        }

        static public bool IsRegexValid(string number)
        {
            return new Regex(@"^\(?[1-9]{2}\)? ?(?:[2-8]|9[1-9])[0-9]{3}\-?[0-9]{4}$").IsMatch(number);
        }
    }
}
