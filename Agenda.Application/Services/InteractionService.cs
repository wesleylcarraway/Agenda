using Agenda.Application.Interfaces;
using Agenda.Application.ViewModels.Interaction;
using Agenda.Domain.Interfaces;
using AutoMapper;

namespace Agenda.Application.Services
{
    public class InteractionService : IInteractionService
    {
        private readonly IMapper _mapper;
        private readonly IInteractionRepository _interactionRepository;

        public InteractionService(IInteractionRepository interactionRepository, IMapper mapper)
        {
            _interactionRepository = interactionRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<InteractionResponse>> SaveJsonInteractionsAsync()
        {
            return _mapper.Map<IEnumerable<InteractionResponse>>(await _interactionRepository.SaveJsonInteractionsAsync());
        }
    }
}
