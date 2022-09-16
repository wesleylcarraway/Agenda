using System.ComponentModel.DataAnnotations;

namespace Agenda.MVC.ViewModels
{
    public class ContactFormViewModel
    {
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }
        public List<PhoneFormViewModel> Phones { get; set; } = new List<PhoneFormViewModel>();
    }
}
