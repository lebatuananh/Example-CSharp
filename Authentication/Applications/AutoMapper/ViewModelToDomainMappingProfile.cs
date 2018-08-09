using Authentication.Applications.ViewModel;
using Authentication.Entity;
using AutoMapper;

namespace Authentication.Applications.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<FunctionViewModel, Function>().ConstructUsing(c => new Function());
            CreateMap<PersonViewModel, Person>().ConstructUsing(c => new Person());
            CreateMap<PermissionViewModel, Permission>().ConstructUsing(c => new Permission());
            CreateMap<AppUserViewModel, AppUser>().ConstructUsing(c => new AppUser());
            CreateMap<AppRoleViewModel, AppRole>().ConstructUsing(x => new AppRole());
        }
    }
}