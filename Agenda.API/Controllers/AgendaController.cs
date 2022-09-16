using Agenda.Application.Interfaces;
using Agenda.Application.Params;
using Agenda.Application.ViewModels;
using Agenda.Application.ViewModels.Contact;
using Agenda.Application.ViewModels.PhoneType;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Agenda.API.Controllers
{
    [ApiController]
    [Route("api/agenda")]
    [Authorize]
    public class AgendaController : ControllerBase
    {
        private readonly IAgendaService _agendaService;
        public AgendaController(IAgendaService agendaService)
        {
            _agendaService = agendaService;
        }

        [HttpGet]
        public async Task<PaginationResponse<ContactResponse>> GetAsync([FromQuery] ContactParams queryParams)
        {
            return new PaginationResponse<ContactResponse> {
                Data = await _agendaService.GetAsync(queryParams),
                Total = await _agendaService.CountAsync(queryParams),
                Take = queryParams.Take,
                Skip = queryParams.Skip,
            };
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ContactResponse>> GetByIdAsync(int id)
        {
            return await _agendaService.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<ContactResponse>> PostAsync([FromBody] ContactRequest contact)
        {
            return await _agendaService.AddAsync(contact);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ContactResponse>> PutAsync([FromBody] ContactRequest contact, [FromRoute] int id)
        {
            return await _agendaService.UpdateAsync(contact, id);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ContactResponse>> DeleteAsync(int id)
        {
            return await _agendaService.RemoveAsync(id);
        }

        [HttpGet("phone-types")]
        public IEnumerable<PhoneTypeResponse> GetPhoneTypes()
        {
            return _agendaService.GetPhoneTypes();
        }
    }
}
