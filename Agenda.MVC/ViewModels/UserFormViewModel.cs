using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Agenda.MVC.ViewModels
{
    public class UserFormViewModel
    {
        public int? Id { get; set; }

        [Required]
        [MinLength(3)]
        [DisplayName("Name")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email")]
        public string Email { get; set; }

        [Required]
        [MinLength(3)]
        [DisplayName("Username")]
        public string Username { get; set; }

        [Required]
        [MinLength(3)]
        [DisplayName("Password")]
        public string Password { get; set; }

        [Required]
        [DisplayName("Role")]
        public int UserRoleId { get; set; }
    }
}
