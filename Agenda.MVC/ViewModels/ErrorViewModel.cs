namespace Agenda.MVC.ViewModels
{
    public class ErrorViewModel
    {
        public string PropertyName { get; set; }
        //
        // Resumo:
        //     The error message
        public string ErrorMessage { get; set; }
        //
        // Resumo:
        //     The property value that caused the failure.
        public object AttemptedValue { get; set; }
        //
        // Resumo:
        //     Custom state associated with the failure.
        public object CustomState { get; set; }
        //
        // Resumo:
        //     Custom severity level associated with the failure.
        //
        // Resumo:
        //     Gets or sets the error code.
        public string ErrorCode { get; set; }
    }
}
