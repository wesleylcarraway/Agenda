using Microsoft.EntityFrameworkCore;
using Agenda.Application.ViewModels.Contact;
using Agenda.Domain.Interfaces;
using FluentValidation;

namespace Agenda.Application.Validations
{
    public class AdminContactRequestValidator : ContactBaseValidator<AdminContactRequest>
    {
        public AdminContactRequestValidator(
            IUserRepository userRepository,
            IContactRepository contactRepository
        ) : base(contactRepository)
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .NotEqual(0);

            RuleFor(x => x.UserId)
                .MustAsync((userId, cancellationToken) => userRepository.Query().AsNoTracking().AnyAsync(x => x.Id == userId, cancellationToken))
                .WithMessage("{PropertyName} Id not found.");

            RuleForEach(x => x.Phones)
                .SetValidator(x => new PhoneRequestValidator(contactRepository, x.UserId));
        }
    }
}
