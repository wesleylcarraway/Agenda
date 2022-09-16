using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Agenda.MVC.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [MinLength(3)]
        [DisplayName("Username")]
        public string Username { get; set; }

        [Required]
        [MinLength(3)]
        [DisplayName("Password")]
        public string Password { get; set; }
    }
}
