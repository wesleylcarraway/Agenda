namespace AgendaConsole.Models.Dtos
{
    public class ContactDto
    {
        public string Name { get; set; }

        public ContactDto(string name)
        {
            Name = name;
        }
    }
}
