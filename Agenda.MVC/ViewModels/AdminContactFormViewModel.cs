using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Agenda.MVC.ViewModels
{
    public class AdminContactFormViewModel : ContactFormViewModel
    {
        [Required]
        [DisplayName("User")]
        public int UserId { get; set; }
    }
}
