using Microsoft.AspNetCore.Http;

namespace Agenda.Application.Interfaces
{
    public interface IUploadFileService
    {
        Task<IEnumerable<IFormFile>> SendFile(IEnumerable<IFormFile> files);
    }
}