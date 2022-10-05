using Agenda.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Agenda.Application.Services
{
    public class UploadFileService: IUploadFileService
    {
        public async Task<IEnumerable<IFormFile>> SendFile(IEnumerable<IFormFile> files)
        {
            foreach (var file in files)
            {
                // Define um nome para o file enviado incluindo o sufixo obtido de milesegundos
                string fileName = "Usuario_arquivo";
                //verifica qual o tipo de file : jpg, gif, png, pdf ou tmp
                if (file.FileName.Contains(".jpg"))
                    fileName += ".png";
                else if (file.FileName.Contains(".gif"))
                    fileName += ".gif";
                else if (file.FileName.Contains(".png"))
                    fileName += ".png";
                else if (file.FileName.Contains(".pdf"))
                    fileName += ".pdf";
                else
                    fileName += ".tmp";

                string assetsFolderPath = @"C:\Users\wesle\Luby\Agenda2.0\Agenda\Agenda.Angular\agendaangular\src\assets";
                
                string fileOriginalPath = assetsFolderPath + "\\" + fileName;
                    
                using (var stream = new FileStream(fileOriginalPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            return files;
        }
    }
}