namespace IdentityFrameworkExample.Application.AutoMapper
{
    using global::AutoMapper;

    using IdentityFrameworkExample.Application.ViewModel;
    using IdentityFrameworkExample.Entity;

    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            this.CreateMap<AppUserViewModel, AppUser>().ConstructUsing(c => new AppUser());
            this.CreateMap<AppRoleViewModel, AppRole>().ConstructUsing(x => new AppRole());
        }
    }
}