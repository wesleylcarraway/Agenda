using Agenda.Application.Interfaces;
using Agenda.Domain.Models;
using Agenda.Domain.Interfaces;
using AutoMapper;
using Agenda.Application.Params;
using Agenda.Domain.Models.Enumerations;
using Agenda.Application.ViewModels.Contact;
using FluentValidation;
using Agenda.Application.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Application.Services
{
    public class AgendaAdminService : IAgendaAdminService
    {
        private readonly IContactRepository _contactRepository;
        private readonly IInteractionRepository _interactionRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unityOfWork;
        private readonly IAuthService _authService;
        private readonly IValidator<AdminContactRequest> _validator;

        public AgendaAdminService
        (
            IContactRepository contactRepository,
            IInteractionRepository interactionRepository,
            IMapper mapper,
            IUnitOfWork unityOfWork,
            IAuthService authService,
            IValidator<AdminContactRequest> validator
            )
        {
            _contactRepository = contactRepository;
            _interactionRepository = interactionRepository;
            _mapper = mapper;
            _unityOfWork = unityOfWork;
            _authService = authService;
            _validator = validator;

            _contactRepository.AddPreQuery(x => x
                .Include(c => c.Phones)
                .ThenInclude(c => c.PhoneType)
                .Include(c => c.User)
                .ThenInclude(c => c.UserRole));
        }

        public async Task<IEnumerable<AdminContactResponse>> GetAsync(AdminContactParams queryParams = null)
        {
            return _mapper.Map<IEnumerable<AdminContactResponse>>(await _contactRepository.GetAllAsync(queryParams.Skip, queryParams.Take, queryParams.Filter()));
        }

        public async Task<AdminContactResponse> GetByIdAsync(int id)
        {
            return _mapper.Map<AdminContactResponse>(await _contactRepository.GetByIdAsync(id));
        }

        public async Task<AdminContactResponse> AddAsync(AdminContactRequest adminContactRequest)
        {
            var validation = await _validator.ValidateAsync(adminContactRequest);
            if (!validation.IsValid)
                throw new BadRequestException(validation);

            var contact = _mapper.Map<Contact>(adminContactRequest);
            await _contactRepository.AddAsync(contact);
            await _interactionRepository.AddAsync(new Interaction(_authService.AuthUser.Id, InteractionType.CreateContact.Id,  "Creating Contact"));
            await _unityOfWork.CommitAsync();
            return _mapper.Map<AdminContactResponse>(contact);
        }

        public async Task<AdminContactResponse> UpdateAsync(AdminContactRequest adminContactRequest, int id)
        {
            var existing = await _contactRepository.GetByIdAsync(id);
            if (existing is null)
                throw new Exception($"Contact with Id: {id} does not exists! ");

            _contactRepository.AddPreQuery(x => x.Where(c => c.Id != id));
            var validation = await _validator.ValidateAsync(adminContactRequest);
            if (!validation.IsValid)
                throw new BadRequestException(validation);

            _mapper.Map<ContactRequest, Contact>(adminContactRequest, existing);
            await _contactRepository.UpdateAsync(existing);
            await _interactionRepository.AddAsync(new Interaction(_authService.AuthUser.Id, InteractionType.UpdateContact.Id,  "Updating Contact"));
            await _unityOfWork.CommitAsync();
            return _mapper.Map<AdminContactResponse>(existing);
        }

        public async Task<AdminContactResponse> RemoveAsync(int id)
        {
            var existing = await _contactRepository.GetByIdAsync(id);
            if (existing is null)
                throw new Exception($"Contact with Id: {id} does not exists! ");

            await _contactRepository.DeleteAsync(id);
            await _interactionRepository.AddAsync(new Interaction(_authService.AuthUser.Id, InteractionType.RemoveContact.Id,  "Removing Contact"));
            await _unityOfWork.CommitAsync();
            return _mapper.Map<AdminContactResponse>(existing);
        }

        public bool PhoneNumberExists(string number)
        {
            var contacts = GetAsync(new AdminContactParams()).GetAwaiter().GetResult().ToList();
            return contacts.Any(contact => contact.Phones.Any(p => p.FormattedNumber == number));
        }
    }
}
