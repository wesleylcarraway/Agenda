namespace Agenda.MVC.ViewModels
{
    public class PhoneViewModel : BaseViewModel
    {
        public string Description { get; set; }
        public string FormattedNumber { get; set; }
        public EnumerationViewModel PhoneType { get; set; }
    }
}
