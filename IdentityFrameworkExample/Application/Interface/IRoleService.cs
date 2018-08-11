namespace IdentityFrameworkExample.Application.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using IdentityFrameworkExample.Application.ViewModel;

    public interface IRoleService
    {
        Task<bool> AddAsync(AppRoleViewModel roleViewModel);

        Task DeleteAsync(Guid id);

        Task<List<AppRoleViewModel>> GetAllAsync();

        Task<AppRoleViewModel> GetByIdAsync(Guid id);

        Task UpdateAsync(AppRoleViewModel roleViewModel);

    }
}
