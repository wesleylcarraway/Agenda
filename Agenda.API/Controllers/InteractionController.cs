using Agenda.Application.Interfaces;
using Agenda.Application.ViewModels.Interaction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Agenda.API.Controllers
{
    [ApiController]
    [Route("api/interactions")]
    [Authorize]
    public class InteractionController : ControllerBase
    {
        private readonly IInteractionService _interactionService;
        public InteractionController(IInteractionService interactionService)
        {
            _interactionService = interactionService;
        }
        [HttpPost]
        public async Task<IEnumerable<InteractionResponse>> SaveInteractionsAsync()
        {
            return await _interactionService.SaveJsonInteractionsAsync();
        }
    }
}
