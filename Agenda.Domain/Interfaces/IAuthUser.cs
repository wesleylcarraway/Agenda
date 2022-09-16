namespace Agenda.Domain.Interfaces
{
    public interface IAuthUser
    {
        int Id { get; }
        string Name { get; }
        string Email { get; }
    }
}
