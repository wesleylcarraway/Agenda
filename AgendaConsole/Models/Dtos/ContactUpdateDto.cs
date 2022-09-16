namespace AgendaConsole.Models.Dtos
{
    public class ContactUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ContactUpdateDto(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
