namespace IdentityFrameworkExample.Application.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using IdentityFrameworkExample.Application.ViewModel;

    public interface IUserService
    {
        Task<bool> AddAsync(AppUserViewModel userVm);

        Task DeleteAsync(Guid id);

        //Task<List<AppUserViewModel>> GetAllAsync();
        List<AppUserViewModel> GetAll();
        Task<AppUserViewModel> GetById(Guid id);


        Task UpdateAsync(AppUserViewModel userVm);
    }
}
