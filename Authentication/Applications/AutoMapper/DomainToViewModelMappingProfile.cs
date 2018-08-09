using Authentication.Applications.ViewModel;
using Authentication.Entity;
using AutoMapper;

namespace Authentication.Applications.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Person, PersonViewModel>().MaxDepth(2);
            CreateMap<Function, FunctionViewModel>().MaxDepth(2);
            CreateMap<Permission, PermissionViewModel>().MaxDepth(2);
            CreateMap<AppRole, AppRoleViewModel>().MaxDepth(2);
            CreateMap<AppUser, AppUserViewModel>().MaxDepth(2);
        }
    }
}