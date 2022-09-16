using Agenda.MVC.ViewModels;
using AutoMapper;

namespace Agenda.MVC.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserViewModel, UserFormViewModel>()
                .ForMember(fvm => fvm.UserRoleId, m => m.MapFrom(vm => vm.UserRole.Id));

            CreateMap<ContactViewModel, ContactFormViewModel>();

            CreateMap<AdminContactViewModel, AdminContactFormViewModel>()
                .ForMember(fvm => fvm.UserId, m => m.MapFrom(vm => vm.User.Id));

            CreateMap<PhoneViewModel, PhoneFormViewModel>()
                .ForMember(fvm => fvm.PhoneTypeId, m => m.MapFrom(vm => vm.PhoneType.Id));
        }
    }
}
