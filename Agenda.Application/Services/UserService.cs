using Agenda.Application.Params;
using Agenda.Application.Interfaces;
using Agenda.Application.ViewModels.User;
using Agenda.Application.ViewModels.UserRole;
using Agenda.Domain.Models;
using Agenda.Domain.Interfaces;
using AutoMapper;
using Agenda.Domain.Models.Enumerations;
using Agenda.Infrastructure.Utils;
using Agenda.Domain.Core;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using Agenda.Application.Exceptions;

namespace Agenda.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unityOfWork;
        private readonly IValidator<UserRequest> _validator;

        public UserService
        (
            IUserRepository userRepository,
            IMapper mapper,
            IUnitOfWork unityOfWork,
            IValidator<UserRequest> validator
            )
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _unityOfWork = unityOfWork;
            _validator = validator;
            _userRepository.AddPreQuery(x => x.Include(m => m.UserRole));
        }

        public async Task<UserResponse> AddAsync(UserRequest userRequest)
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

        public async Task<IEnumerable<UserResponse>> GetAsync(UserParams queryParams = null)
        {
            return _mapper.Map<IEnumerable<UserResponse>>(await _userRepository.GetAllAsync(queryParams.Skip, queryParams.Take, queryParams.Filter()));
        }

        public async Task<IEnumerable<UserResponse>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<UserResponse>>(
                await _userRepository.GetAllUsersAsync());
        }

        public async Task<UserResponse> GetByIdAsync(int id)
        {
            return _mapper.Map<UserResponse>(await _userRepository.GetByIdAsync(id));
        }

        public IEnumerable<UserRoleResponse> GetUserRoles()
        {
            return _mapper.Map<IEnumerable<UserRoleResponse>>(Enumeration.GetAll<UserRole>());
        }

        public async Task<UserResponse> RemoveAsync(int id)
        {
            var existing = await _userRepository.GetByIdAsync(id);
            if (existing is null)
                throw new Exception($"User with Id: {id} does not exists! ");

            await _userRepository.DeleteAsync(id);
            await _unityOfWork.CommitAsync();
            return _mapper.Map<UserResponse>(existing);
        }

        public async Task<UserResponse> UpdateAsync(UserRequest userRequest, int id)
        {
            var existing = await _userRepository.GetByIdAsync(id);
            if (existing is null)
                throw new Exception($"User with Id: {id} does not exists! ");

            _userRepository.AddPreQuery(x => x.Where(u => u.Id != id));
            userRequest.Password = string.IsNullOrEmpty(userRequest.Password) ? existing.Password : PasswordHasher.Hash(userRequest.Password);

            var validation = await _validator.ValidateAsync(userRequest);
            if (!validation.IsValid)
            {
                validation.Errors.ForEach(x => Console.WriteLine(x.ErrorMessage));
                throw new BadRequestException(validation);
            }


            _mapper.Map<UserRequest, User>(userRequest, existing);
            await _userRepository.UpdateAsync(existing);

            await _unityOfWork.CommitAsync();
            return _mapper.Map<UserResponse>(existing);
        }
    }
}
