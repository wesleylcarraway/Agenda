using System.Security.Claims;
using Agenda.Application.Exceptions;
using Agenda.Application.Interfaces;
using Agenda.Application.ViewModels.Login;
using Agenda.Domain.Interfaces;
using Agenda.Infrastructure.Utils;
using FluentValidation;

namespace Agenda.Application.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<LoginRequest> _validator;

        public LoginService(
            IValidator<LoginRequest> validator,
            IUserRepository userRepository)
        {
            _validator = validator;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<Claim>> Login(LoginRequest model)
        {
            var validation = (await _validator.ValidateAsync(model));
            if (!validation.IsValid)
                throw new BadRequestException(validation);

            var user = await _userRepository.GetUserUserRole(model.Username);

            if (user is null)
                throw new BadRequestException(nameof(model.Username), "User not found.");

            if (!PasswordHasher.Verify(model.Password, user.Password))
                throw new BadRequestException(nameof(model.Password), "Invalid password");

            return new List<Claim>()
            {
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.UserRole.Name),
            };
        }
    }
}
