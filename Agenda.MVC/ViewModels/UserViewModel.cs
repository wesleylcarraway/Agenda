namespace Agenda.MVC.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public EnumerationViewModel UserRole { get; set; }
    }
}
