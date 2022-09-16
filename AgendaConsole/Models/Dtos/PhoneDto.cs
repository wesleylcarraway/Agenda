using System.Text.RegularExpressions;

namespace AgendaConsole.Models.Dtos
{
    public class PhoneDto
    {
        public int ContactId { get; set; }
        public string Description { get; set; }
        public string FormattedPhone { get; set; }
        public int Ddd { get; set; }
        public string Number { get; set; }

        public PhoneDto(int contactId, string description, string formattedPhone)
        {
            ContactId = contactId;
            Description = description;
            Ddd = int.Parse(formattedPhone.Substring(1, 2));
            Number = Regex.Replace(formattedPhone.Substring(4), @"[^\d]", "");
            FormattedPhone = formattedPhone;
        }
    }
}
