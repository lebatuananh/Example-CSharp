using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Applications
{
    using Authentication.Applications.ViewModel;
    using Authentication.Entity;
    using Authentication.Entity.EF;

    using global::AutoMapper;
    using global::AutoMapper.QueryableExtensions;

    using Microsoft.AspNetCore.Identity;

    public class PermissionsService:IPermissionService
    {
        private IRepository<Function, Guid> _functionRepository;
        private IRepository<Permission, Guid> _permissionRepository;
        private RoleManager<AppRole> _roleManager;
        private UserManager<AppUser> _userManager;
        private IUnitOfWork _unitOfWork;

        public PermissionsService(IRepository<Function, Guid> functionRepository, IRepository<Permission, Guid> permissionRepository, RoleManager<AppRole> roleManager, UserManager<AppUser> userManager, IUnitOfWork unitOfWork)
        {
            this._functionRepository = functionRepository;
            this._permissionRepository = permissionRepository;
            this._roleManager = roleManager;
            this._userManager = userManager;
            this._unitOfWork = unitOfWork;
        }

        public ICollection<PermissionViewModel> GetByFunctionId(Guid functionId)
        {
            return _permissionRepository
                .GetAll().Where(x => x.FunctionId == functionId)
                .ProjectTo<PermissionViewModel>().ToList();
        }

        public async Task<List<PermissionViewModel>> GetByUserId(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var roles = await _userManager.GetRolesAsync(user);

            var query = (from f in _functionRepository.GetAll()
                         join p in _permissionRepository.GetAll() on f.Id equals p.FunctionId
                         join r in _roleManager.Roles on p.RoleId equals r.Id
                         where roles.Contains(r.Name)
                         select p);

            return query.ProjectTo<PermissionViewModel>().ToList();
        }

        public void Add(PermissionViewModel permissionViewModel)
        {
            var permission = Mapper.Map<PermissionViewModel, Permission>(permissionViewModel);
            _permissionRepository.Insert(permission);
        }

        public void DeleteAll(Guid functionId)
        {
            _permissionRepository.Delete(x => x.FunctionId == functionId);
        }

        public void SaveChange()
        {
            this._unitOfWork.Commit();
        }
    }
}
