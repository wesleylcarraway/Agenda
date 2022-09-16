using Agenda.Application.Utils;
using Agenda.Application.ViewModels.User;
using Agenda.Domain.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Application.Validations
{
    public class UserBaseRequestValidator<T> : AbstractValidator<T> where T : UserRequest
    {
        public UserBaseRequestValidator(IUserRepository userRepository)
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .NotEmpty();

            RuleFor(x => x.Email)
                .MustAsync((email, cancelToken) => userRepository.Query().AsNoTracking().AllAsync(x => x.Email != email, cancelToken))
                .WithMessage("{PropertyName} A user with informed email already exists.");

            RuleFor(x => x.Username)
                .MinimumLength(3)
                .MaximumLength(50)
                .NotEmpty();

            RuleFor(x => x.Username)
                .MustAsync((username, cancelToken) => userRepository.Query().AsNoTracking().AllAsync(x => x.Username != username, cancelToken))
                .WithMessage("{PropertyName} A user with informed username already exists.");

            RuleFor(x => x.Name)
                .MinimumLength(3)
                .MaximumLength(50)
                .NotEmpty();

            RuleFor(x => x.Password)
                .MinimumLength(8)
                .MaximumLength(200)
                .NotEmpty();
        }
    }
}
