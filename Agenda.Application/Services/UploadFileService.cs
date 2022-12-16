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
                // < define a pasta onde vamos salvar os files >
                string folder = "Arquivos_Usuario";
                // Define um nome para o file enviado incluindo o sufixo obtido de milesegundos
                string fileName = "Usuario_arquivo_" + DateTime.Now.Millisecond.ToString();
                //verifica qual o tipo de file : jpg, gif, png, pdf ou tmp
                if (file.FileName.Contains(".jpg"))
                    fileName += ".jpg";
                else if (file.FileName.Contains(".gif"))
                    fileName += ".gif";
                else if (file.FileName.Contains(".png"))
                    fileName += ".png";
                else if (file.FileName.Contains(".pdf"))
                    fileName += ".pdf";
                else
                    fileName += ".tmp";
                //< obtém o diretório atual >
                string currentDir = Directory.GetCurrentDirectory();
                // monta o caminho onde vamos salvar o file : 
                // ~..\Arquivos\Arquivos_Usuario\Recebidos
                string filePath = currentDir + "\\Arquivos\\" + folder + "\\";
                // incluir a pasta Recebidos e o nome do file enviado : 
                // ~..\Arquivos\Arquivos_Usuario\Recebidos\
                string fileOriginalPath = filePath + "\\Recebidos\\" + fileName;
                //copia o file para o local de destino original
                using (var stream = new FileStream(fileOriginalPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            return files;
        }
    }
}