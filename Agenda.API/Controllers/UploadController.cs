using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Agenda.API.Controllers
{
    [ApiController]
    [Route("api/upload")]
    [Authorize]
    public class UploadController : ControllerBase
    {
        //método para enviar os arquivos usando a interface IFormFile
        [HttpPost]
        public async Task<IEnumerable<IFormFile>> SendFile(IEnumerable<IFormFile> files)
        {
            long tamanhoArquivos = files.Sum(f => f.Length);
            // caminho completo do arquivo na localização temporária
            var caminhoArquivo = Path.GetTempFileName();

            // processa os arquivo enviados
            //percorre a lista de files selecionados
            foreach (var arquivo in files)
            {
                //verifica se existem files 
                if (arquivo == null || arquivo.Length == 0)
                {
                    //retorna a viewdata com erro
                    //ViewData["Erro"] = "Error: Arquivo(s) não selecionado(s)";
                    //return View(ViewData);
                }
                // < define a pasta onde vamos salvar os files >
                string pasta = "Arquivos_Usuario";
                // Define um nome para o arquivo enviado incluindo o sufixo obtido de milesegundos
                string nomeArquivo = "Usuario_arquivo_" + DateTime.Now.Millisecond.ToString();
                //verifica qual o tipo de arquivo : jpg, gif, png, pdf ou tmp
                if (arquivo.FileName.Contains(".jpg"))
                    nomeArquivo += ".jpg";
                else if (arquivo.FileName.Contains(".gif"))
                    nomeArquivo += ".gif";
                else if (arquivo.FileName.Contains(".png"))
                    nomeArquivo += ".png";
                else if (arquivo.FileName.Contains(".pdf"))
                    nomeArquivo += ".pdf";
                else
                    nomeArquivo += ".tmp";
                //< obtém o caminho físico da pasta wwwroot >
                string caminho_WebRoot = "/data";
                // monta o caminho onde vamos salvar o arquivo : 
                // ~\wwwroot\Arquivos\Arquivos_Usuario\Recebidos
                string caminhoDestinoArquivo = caminho_WebRoot + "\\Arquivos\\" + pasta + "\\";
                // incluir a pasta Recebidos e o nome do arquivo enviado : 
                // ~\wwwroot\Arquivos\Arquivos_Usuario\Recebidos\
                string caminhoDestinoArquivoOriginal = caminhoDestinoArquivo + "\\Recebidos\\" + nomeArquivo;
                //copia o arquivo para o local de destino original
                using (var stream = new FileStream(caminhoDestinoArquivoOriginal, FileMode.Create))
                {
                    await arquivo.CopyToAsync(stream);
                }
            }

            return files;
            
        }
    }
}