namespace Agenda.Domain.Core
{
    public abstract class Register
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
