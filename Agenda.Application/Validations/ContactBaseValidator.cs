using Agenda.Application.ViewModels.Contact;
using Agenda.Domain.Interfaces;
using FluentValidation;

namespace Agenda.Application.Validations
{
    public class ContactBaseValidator<T> : AbstractValidator<T> where T : ContactRequest
    {
        public ContactBaseValidator(IContactRepository contactRepository)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Phones)
                .Must(phones => phones.GroupBy(x => x.FormattedNumber).All(group => group.Count() == 1))
                .WithMessage("There are duplicate phones in the list.");
        }
    }
}
