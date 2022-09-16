using AutoMapper;
using Agenda.Domain.Models;
using Agenda.Domain.Models.Enumerations;
using Agenda.Application.Extensions;
using Agenda.Application.Utils;
using Agenda.Application.ViewModels.User;
using Agenda.Application.ViewModels.UserRole;
using Agenda.Application.ViewModels.Contact;
using Agenda.Application.ViewModels.Phone;
using Agenda.Application.ViewModels.PhoneType;
using Agenda.Application.ViewModels.Interaction;
using Agenda.Application.ViewModels.InteractionType;

namespace Agenda.Application.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Contact, ContactResponse>();
            CreateMap<Contact, AdminContactResponse>();
            CreateMap<Phone, PhoneResponse>();
            CreateMap<PhoneType, PhoneTypeResponse>();
            CreateMap<User, UserResponse>();
            CreateMap<UserRole, UserRoleResponse>();
            CreateMap<Interaction, InteractionResponse>();
            CreateMap<InteractionType, InteractionTypeResponse>();

            CreateMap<ContactRequest, Contact>()
                .MergeList(x => x.Phones, vm => vm.Phones);

            CreateMap<AdminContactRequest, Contact>()
                .ForMember(x => x.UserId, m => m.MapFrom(req => req.UserId))
                .MergeList(x => x.Phones, vm => vm.Phones);

            CreateMap<PhoneRequest, Phone>()
                .ForMember(x => x.DDD, m => m.MapFrom(req => FormatPhone.Split(req.FormattedNumber).Item1))
                .ForMember(x => x.Number, m => m.MapFrom(req => FormatPhone.Split(req.FormattedNumber).Item2));

            CreateMap<UserRequest, User>();
            CreateMap<CommonUserRequest, User>();
        }
    }
}
