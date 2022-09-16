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
using Agenda.Application.ViewModels.PhoneType;
using Agenda.Domain.Core;

namespace Agenda.Application.Services
{
    public class AgendaService : IAgendaService
    {
        private readonly IContactRepository _contactRepository;
        private readonly IInteractionRepository _interactionRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unityOfWork;
        private readonly IAuthService _authService;
        private readonly IValidator<ContactRequest> _validator;

        public AgendaService
        (
            IContactRepository contactRepository,
            IInteractionRepository interactionRepository,
            IMapper mapper,
            IUnitOfWork unityOfWork,
            IAuthService authService,
            IValidator<ContactRequest> validator
            )
        {
            _contactRepository = contactRepository;
            _interactionRepository = interactionRepository;
            _mapper = mapper;
            _unityOfWork = unityOfWork;
            _authService = authService;
            _validator = validator;

            _contactRepository.AddPreQuery(x => x
                .Where(c => c.UserId == _authService.AuthUser.Id)
                .Include(x => x.Phones)
                .ThenInclude(x => x.PhoneType));
        }

        public async Task<IEnumerable<ContactResponse>> GetAsync(ContactParams queryParams)
        {
            return _mapper.Map<IEnumerable<ContactResponse>>(await _contactRepository.GetAllAsync(queryParams.Skip, queryParams.Take, queryParams.Filter()));
        }

        public async Task<ContactResponse> GetByIdAsync(int id)
        {
            return _mapper.Map<ContactResponse>(await _contactRepository.GetByIdAsync(id));
        }

        public async Task<ContactResponse> AddAsync(ContactRequest contactRequest)
        {
            var validation = await _validator.ValidateAsync(contactRequest);
            if (!validation.IsValid)
                throw new BadRequestException(validation);

            var contact = _mapper.Map<Contact>(contactRequest);
            contact.UserId = _authService.AuthUser.Id;
            await _contactRepository.AddAsync(contact);
            await _interactionRepository.AddAsync(new Interaction(_authService.AuthUser.Id, InteractionType.CreateContact.Id,  "Creating Contact"));
            await _unityOfWork.CommitAsync();
            return _mapper.Map<ContactResponse>(contact);
        }

        public async Task<ContactResponse> UpdateAsync(ContactRequest contactRequest, int id)
        {
            var existing = await _contactRepository.GetByIdAsync(id);
            if (existing is null)
                throw new Exception($"Contact with Id: {id} does not exists! ");

            if (existing.UserId != _authService.AuthUser.Id)
                throw new NotAuthorizedException();

            _contactRepository.AddPreQuery(x => x.Where(c => c.Id != id));
            var validation = await _validator.ValidateAsync(contactRequest);
            if (!validation.IsValid)
                throw new BadRequestException(validation);

            _mapper.Map<ContactRequest, Contact>(contactRequest, existing);
            await _contactRepository.UpdateAsync(existing);
            await _interactionRepository.AddAsync(new Interaction(_authService.AuthUser.Id, InteractionType.UpdateContact.Id,  "Updating Contact"));
            await _unityOfWork.CommitAsync();
            return _mapper.Map<ContactResponse>(existing);
        }

        public async Task<ContactResponse> RemoveAsync(int id)
        {
            var existing = await _contactRepository.GetByIdAsync(id);
            if (existing is null)
                throw new Exception($"Contact with Id: {id} does not exists! ");

            if (existing.UserId != _authService.AuthUser.Id)
                throw new NotAuthorizedException();

            await _contactRepository.DeleteAsync(id);
            await _interactionRepository.AddAsync(new Interaction(_authService.AuthUser.Id, InteractionType.RemoveContact.Id,  "Removing Contact"));
            await _unityOfWork.CommitAsync();
            return _mapper.Map<ContactResponse>(existing);
        }

        public async Task<int> CountAsync(ContactParams queryParams)
        {
            return await _contactRepository.CountAsync(queryParams.Filter());
        }

        public IEnumerable<PhoneTypeResponse> GetPhoneTypes()
        {
            return _mapper.Map<IEnumerable<PhoneTypeResponse>>(Enumeration.GetAll<PhoneType>());
        }
        public bool PhoneNumberExists(string number)
        {
            var contacts = GetAsync(new ContactParams()).GetAwaiter().GetResult().ToList();
            return contacts.Any(contact => contact.Phones.Any(p => p.FormattedNumber == number));
        }
    }
}
