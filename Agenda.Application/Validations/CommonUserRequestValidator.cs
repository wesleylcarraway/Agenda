using Agenda.Application.ViewModels.User;
using Agenda.Domain.Interfaces;
using FluentValidation;

namespace Agenda.Application.Validations
{
    public class CommonUserRequestValidator : UserBaseRequestValidator<CommonUserRequest>
    {
        public CommonUserRequestValidator(IUserRepository userRepository) : base(userRepository)
        {
            RuleFor(x => x.UserRoleId)
                .Equal(2)
                .WithMessage("{Propertyname} You can only create common user.");
        }
    }
}
