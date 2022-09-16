namespace Agenda.MVC.ViewModels
{
    public class ContactViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public IEnumerable<PhoneViewModel> Phones { get; set; } = new List<PhoneViewModel>();
    }
}
