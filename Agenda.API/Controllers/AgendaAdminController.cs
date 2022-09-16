using Agenda.Application.Interfaces;
using Agenda.Application.Params;
using Agenda.Application.ViewModels;
using Agenda.Application.ViewModels.Contact;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Agenda.API.Controllers
{
    [ApiController]
    [Route("api/admin/agenda")]
    [Authorize(Roles = "Admin")]
    public class AgendaAdminController : ControllerBase
    {
        private readonly IAgendaAdminService _agendaAdminService;

        public AgendaAdminController(IAgendaAdminService agendaAdminService)
        {
            _agendaAdminService = agendaAdminService;
        }

        [HttpGet]
        public async Task<PaginationResponse<AdminContactResponse>> GetAsync([FromQuery] AdminContactParams queryParams)
        {
            var data = await _agendaAdminService.GetAsync(queryParams);
            return new PaginationResponse<AdminContactResponse> {
                Data = data,
                Total = data.Count(),
                Take = queryParams.Take,
                Skip = queryParams.Skip,
            };
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AdminContactResponse>> GetByIdAsync(int id)
        {
            return await _agendaAdminService.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<AdminContactResponse>> PostAsync([FromBody] AdminContactRequest contact)
        {
            return await _agendaAdminService.AddAsync(contact);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<AdminContactResponse>> PutAsync([FromBody] AdminContactRequest contact, [FromRoute] int id)
        {
            return await _agendaAdminService.UpdateAsync(contact, id);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<AdminContactResponse>> DeleteAsync(int id)
        {
            return await _agendaAdminService.RemoveAsync(id);
        }
    }
}
