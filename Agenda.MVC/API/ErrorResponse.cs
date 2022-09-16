using Agenda.MVC.ViewModels;

namespace Agenda.MVC.API
{
    public class ErrorResponse
    {
        public IEnumerable<ErrorViewModel> Errors { get; set; }
        public string Message { get; set; }
    }
}
