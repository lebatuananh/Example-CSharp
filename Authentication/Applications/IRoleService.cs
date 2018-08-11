using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Applications
{
    using Authentication.Applications.ViewModel;
    using Authentication.Utils.Dtos;

    public interface IRoleService
    {
        Task<bool> AddAsync(AppRoleViewModel roleViewModel);

        Task DeleteAsync(Guid id);

        Task<List<AppRoleViewModel>> GetAllAsync();

        PagedResult<AppRoleViewModel> GetAllPagingAsync(string keyword, int page, int pageSize);

        Task<AppRoleViewModel> GetByIdAsync(Guid id);

        Task UpdateAsync(AppRoleViewModel roleViewModel);

        List<PermissionViewModel> GetListFunctionWithRole(Guid roleId);

        void SavePermission(List<PermissionViewModel> permissionViewModels, Guid roleId);
        Task<bool> CheckPermission(string functionCode, string action, string[] roles);

    }
}
