using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Applications
{
    using Authentication.Applications.ViewModel;

    public interface IPermissionService
    {
        ICollection<PermissionViewModel> GetByFunctionId(Guid functionId);

        Task<List<PermissionViewModel>> GetByUserId(Guid userId);

        void Add(PermissionViewModel permissionViewModel);

        void DeleteAll(Guid functionId);

        void SaveChange();
    }
}
