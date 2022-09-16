namespace AgendaConsole.Models
{
    public class Phone
    {
        public int Id { get; set; }
        public int ContactId { get; set; }
        public string Description { get; set; }
        public string FormattedPhone { get; set; }
        public int Ddd { get; set; }
        public string Number { get; set; }

        public override string ToString()
        {
            return  $"{Id} - {Description}: {FormattedPhone}\n";
        }
    }
}
