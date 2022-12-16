using Agenda.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Agenda.API.Controllers
{
    [ApiController]
    [Route("api/upload")]
    [Authorize]
    public class UploadController : ControllerBase
    {
        private readonly IUploadFileService _uploadFileService;

        public UploadController(IUploadFileService uploadFileService)
        {
            _uploadFileService = uploadFileService;
        }
        
        [HttpPost]
        public async Task<IEnumerable<IFormFile>> SendFile(IEnumerable<IFormFile> files)
        {
            return await _uploadFileService.SendFile(files);
        }
    }
}