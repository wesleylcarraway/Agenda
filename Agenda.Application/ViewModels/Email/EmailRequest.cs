namespace Agenda.Application.ViewModels.Email
{
    public class EmailRequest
    {
        public string Body { get; set; }
        public string Subject { get; set; }
        public string To { get; set; }
    }
}