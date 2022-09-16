using Agenda.Application.ViewModels.User;
using Agenda.Domain.Core;
using Agenda.Domain.Interfaces;
using Agenda.Domain.Models.Enumerations;
using FluentValidation;

namespace Agenda.Application.Validations
{
    public class UserRequestValidator : UserBaseRequestValidator<UserRequest>
    {
        public UserRequestValidator(IUserRepository userRepository) : base(userRepository)
        {
            RuleFor(x => x.UserRoleId)
                .Must(type => Enumeration.GetAll<UserRole>().Any(x => x.Id == type))
                .WithMessage("{Propertyname} User role invalid.");
        }
    }
}
