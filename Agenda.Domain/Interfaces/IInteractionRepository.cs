using Agenda.Domain.Models;

namespace Agenda.Domain.Interfaces
{
    public interface IInteractionRepository : IBaseRepository<Interaction>
    {
        Task<IEnumerable<Interaction>> SaveJsonInteractionsAsync();
    }
}
