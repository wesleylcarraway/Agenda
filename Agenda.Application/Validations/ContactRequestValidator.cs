using Agenda.Application.Interfaces;
using Agenda.Application.ViewModels.Contact;
using Agenda.Domain.Interfaces;

namespace Agenda.Application.Validations
{
    public class ContactRequestValidator : ContactBaseValidator<ContactRequest>
    {
        private readonly IAuthService _authService;

        public ContactRequestValidator(
            IContactRepository contactRepository,
            IAuthService authService) : base(contactRepository)
        {
            _authService = authService;

            RuleForEach(x => x.Phones)
                .SetValidator(new PhoneRequestValidator(contactRepository, _authService.AuthUser.Id));
        }
    }
}
