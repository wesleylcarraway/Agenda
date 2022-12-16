
using Agenda.Application.ViewModels.Email;
using FluentValidation;

namespace Agenda.Application.Validations
{
    public class EmailRequestValidator : AbstractValidator<EmailRequest>
    {
        public EmailRequestValidator()
        {
            RuleFor(x => x.To)
                .EmailAddress()
                .NotEmpty();

            RuleFor(x => x.Subject)
                .MinimumLength(3)
                .MaximumLength(50)
                .NotEmpty();

            RuleFor(x => x.Body)
                .MinimumLength(0)
                .MaximumLength(500);
        }
    }
}