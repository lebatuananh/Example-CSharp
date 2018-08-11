using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Applications
{
    using Authentication.Applications.ViewModel;
    using Authentication.Utils.Dtos;

    public interface IUserService
    {
        Task<bool> AddAsync(AppUserViewModel userVm);

        Task DeleteAsync(Guid id);

        //Task<List<AppUserViewModel>> GetAllAsync();
        List<AppUserViewModel> GetAll();
        PagedResult<AppUserViewModel> GetAllPagingAsync(string keyword, int page, int pageSize);

        Task<AppUserViewModel> GetById(Guid id);

        void Save();

        Task UpdateAsync(AppUserViewModel userVm);
    }
}
