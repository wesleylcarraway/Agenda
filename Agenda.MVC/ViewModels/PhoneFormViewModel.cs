using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Agenda.MVC.ViewModels
{
    public class PhoneFormViewModel
    {
        [Required]
        [MinLength(3)]
        [DisplayName("Description")]
        public string Description { get; set; }

        [Required]
        [DisplayName("Phone number")]
        [RegularExpression(@"^\([1-9]{2}\) (?:[2-8]|9[1-9])[0-9]{3}\-[0-9]{4}$", ErrorMessage = "Invalid phone")]
        public string FormattedNumber { get; set; }

        [DisplayName("Phone type")]
        [Required]
        public int PhoneTypeId { get; set; }
    }
}
