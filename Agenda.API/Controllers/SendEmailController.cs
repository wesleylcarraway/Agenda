
using Agenda.Application.Interfaces;
using Agenda.Application.ViewModels.Email;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Agenda.API.Controllers
{
    [ApiController]
    [Route("api/send-email")]
    //[Authorize]
    public class SendEmailController : ControllerBase
    {
        private readonly ISendEmailService _sendEmailService;

        public SendEmailController(ISendEmailService sendEmailService)
        {
            _sendEmailService = sendEmailService;
        }

        [HttpPost]
        public async Task<ActionResult<EmailResponse>> PostAsync([FromBody] EmailRequest email)
        {
            return await _sendEmailService.Send(email);
        }
    }
}