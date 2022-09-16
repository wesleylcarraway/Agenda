using Microsoft.EntityFrameworkCore;
using Agenda.Application.Utils;
using Agenda.Application.ViewModels.Phone;
using Agenda.Domain.Core;
using Agenda.Domain.Interfaces;
using Agenda.Domain.Models.Enumerations;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Agenda.Application.Validations
{
    [IgnoreInjection]
    public class PhoneRequestValidator : AbstractValidator<PhoneRequest>
    {
        private readonly IContactRepository _contactRepository;

        public PhoneRequestValidator(
            IContactRepository contactRepository,
            int userId)
        {
            _contactRepository = contactRepository;

            RuleFor(x => x.Description)
                .MaximumLength(200);

            RuleFor(x => x.PhoneTypeId)
                .Must(type => Enumeration.GetAll<PhoneType>().Any(x => x.Id == type))
                .WithMessage("{PropertyName} Invalid phone type.");
            RuleFor(x => x.FormattedNumber)
                .Must(x => ValidatePhoneNumber.IsValid(x))
                .WithMessage("{PropertyName}: {PropertyValue} - Invalid phone format: (xx) x?xxxx-xxxx");
            RuleFor(x => x.FormattedNumber)
                .MustAsync((phoneNumber, cancelToken) => VerifyExistingPhones(phoneNumber, userId, cancelToken))
                .WithMessage("Phone already exists {PropertyName}: {PropertyValue}");

            RuleFor(x => x.FormattedNumber)
                .Must((phone, formatted, context) =>
                {
                    if (phone.PhoneTypeId == 2)
                    {
                        return new Regex(@"^\(?[1-9][0-9]\)? ?(9[0-9])[0-9]{3}\-?[0-9]{4}$").IsMatch(formatted);
                    }
                    else
                    {
                        return new Regex(@"^\(?[1-9][0-9]\)? ?([1-9])[0-9]{3}\-?[0-9]{4}$").IsMatch(formatted);
                    }
                })
                .WithMessage("{PropertyName}: {PropertyValue} - Invalid phone format. type (xx) 9xxxx-xxxx for mobile and (xx) xxxx-xxxx for residencial.");
        }

        private async Task<bool> VerifyExistingPhones(
            string phoneNumber,
            int userId,
            CancellationToken cancellationToken = default)
        {
            var phones = await _contactRepository
                .Query()
                .Where(x => x.UserId == userId)
                .SelectMany(x => x.Phones)
                .Select(x => x.FormattedNumber)
                .ToListAsync();

            return !phones.Any(x => x == phoneNumber);
        }
    }
}
