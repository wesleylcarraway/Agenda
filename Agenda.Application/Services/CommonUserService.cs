using Agenda.Application.Exceptions;
using Agenda.Application.Interfaces;
using Agenda.Application.ViewModels.User;
using Agenda.Domain.Interfaces;
using Agenda.Domain.Models;
using Agenda.Infrastructure.Utils;
using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Application.Services
{
    public class CommonUserService : ICommonUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IInteractionRepository _interactionRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unityOfWork;
        private readonly IValidator<CommonUserRequest> _validator;

        public CommonUserService
        (
            IUserRepository userRepository,
            IInteractionRepository interactionRepository,
            IMapper mapper,
            IUnitOfWork unityOfWork,
            IValidator<CommonUserRequest> validator
            )
        {
            _userRepository = userRepository;
            _interactionRepository = interactionRepository;
            _mapper = mapper;
            _unityOfWork = unityOfWork;
            _validator = validator;
            _userRepository.AddPreQuery(x => x.Include(m => m.UserRole));
        }

        public async Task<UserResponse> AddAsync(CommonUserRequest userRequest)
        {
            var validation = await _validator.ValidateAsync(userRequest);
            if (!validation.IsValid)
                throw new BadRequestException(validation);

            var user = _mapper.Map<User>(userRequest);
            user.Password = PasswordHasher.Hash(userRequest.Password);
            await _userRepository.AddAsync(user);
            await _unityOfWork.CommitAsync();
            return _mapper.Map<UserResponse>(user);
        }

        public async Task<UserResponse> UpdateAsync(CommonUserRequest userRequest, int id)
        {
            var existing = await _userRepository.GetByIdAsync(id);
            if (existing is null)
                throw new Exception($"User with Id: {id} does not exists! ");

            if(existing.UserRoleId == 1)
                throw new Exception("Only admins can edit admins!");

            _userRepository.AddPreQuery(x => x.Where(u => u.Id != id));
            userRequest.Password = string.IsNullOrEmpty(userRequest.Password) ? existing.Password : PasswordHasher.Hash(userRequest.Password);

            var validation = await _validator.ValidateAsync(userRequest);
            if (!validation.IsValid)
                throw new BadRequestException(validation);

            _mapper.Map(userRequest, existing);
            await _userRepository.UpdateAsync(existing);

            await _unityOfWork.CommitAsync();
            return _mapper.Map<UserResponse>(existing);
        }
    }
}
