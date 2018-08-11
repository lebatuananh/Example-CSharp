namespace IdentityFrameworkExample.Application.AutoMapper
{
    using global::AutoMapper;

    using IdentityFrameworkExample.Application.ViewModel;
    using IdentityFrameworkExample.Entity;

    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            this.CreateMap<AppRole, AppRoleViewModel>().MaxDepth(2);
            this.CreateMap<AppUser, AppUserViewModel>().MaxDepth(2);
        }
    }
}