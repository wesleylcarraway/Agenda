using Agenda.Application.ViewModels.Interaction;

namespace Agenda.Application.Interfaces
{
    public interface IInteractionService
    {
        Task<IEnumerable<InteractionResponse>> SaveJsonInteractionsAsync();
    }
}
