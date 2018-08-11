using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Applications
{
    using Authentication.Applications.ViewModel;
    using Authentication.Entity;
    using Authentication.Entity.EF;
    using Authentication.Utils.Dtos;

    using global::AutoMapper;
    using global::AutoMapper.QueryableExtensions;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// The role service.
    /// </summary>
    public class RoleService : IRoleService
    {
        private RoleManager<AppRole> _roleManager;
        private IRepository<Function, Guid> _functionRepository;
        private IRepository<Permission, Guid> _permissionRepository;
        private IUnitOfWork _unitOfWork;

        public RoleService(RoleManager<AppRole> roleManager, IRepository<Function, Guid> functionRepository, IRepository<Permission, Guid> permissionRepository, IUnitOfWork unitOfWork)
        {
            this._roleManager = roleManager;
            this._functionRepository = functionRepository;
            this._permissionRepository = permissionRepository;
            this._unitOfWork = unitOfWork;
        }

        public async Task<bool> AddAsync(AppRoleViewModel roleViewModel)
        {
            var role = new AppRole()
            {
                Name = roleViewModel.Name,
                Description = roleViewModel.Description,
                CreatedDate = DateTime.Now
            };
            var result = await this._roleManager.CreateAsync(role);
            return result.Succeeded;
        }

        public async Task DeleteAsync(Guid id)
        {
            var role = await this._roleManager.FindByIdAsync(id.ToString());
            await this._roleManager.DeleteAsync(role);
        }

        public async Task<List<AppRoleViewModel>> GetAllAsync()
        {
            return await this._roleManager.Roles.ProjectTo<AppRoleViewModel>().ToListAsync();
        }

        public PagedResult<AppRoleViewModel> GetAllPagingAsync(string keyword, int page, int pageSize)
        {
            var query = this._roleManager.Roles;
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
            }

            int totalRow = query.Count();
            query = query.Skip((page - 1) * pageSize).Take(pageSize);
            var data = query.ProjectTo<AppRoleViewModel>().ToList();
            var paginationSet = new PagedResult<AppRoleViewModel>()
            {
                Results = data,
                CurrentPage = page,
                RowCount = totalRow,
                PageSize = pageSize
            };
            return paginationSet;
        }

        public async Task<AppRoleViewModel> GetByIdAsync(Guid id)
        {
            var role = await this._roleManager.FindByIdAsync(id.ToString());
            return Mapper.Map<AppRole, AppRoleViewModel>(role);
        }

        public async Task UpdateAsync(AppRoleViewModel roleViewModel)
        {
            var role = await this._roleManager.FindByIdAsync(roleViewModel.Id.ToString());
            role.Name = roleViewModel.Name;
            role.Description = roleViewModel.Description;
            role.ModifiedDate=DateTime.Now;
            await this._roleManager.UpdateAsync(role);
        }

        public List<PermissionViewModel> GetListFunctionWithRole(Guid roleId)
        {
            var functions = this._functionRepository.GetAll();
            var permissons = this._permissionRepository.GetAll();
            var query = from f in functions
                        join p in permissons on f.Id equals p.FunctionId into fp
                        from p in fp.DefaultIfEmpty()
                        where p != null && p.RoleId == roleId
                        select new PermissionViewModel() { RoleId = roleId, FunctionId = f.Id };
            return query.ToList();
        }

        public void SavePermission(List<PermissionViewModel> permissionViewModels, Guid roleId)
        {
            var permissions = Mapper.Map<List<PermissionViewModel>, List<Permission>>(permissionViewModels);
            var oldPermissions = this._permissionRepository.GetAll().ToList();
            if (oldPermissions.Count > 0)
            {
                this._permissionRepository.Delete(x => x.RoleId == roleId);
            }

            foreach (var permission in permissions)
            {
                this._permissionRepository.Insert(permission);
            }
            this._unitOfWork.Commit();
        }

        public Task<bool> CheckPermission(string functionCode, string action, string[] roles)
        {
            var functions = this._functionRepository.GetAll();
            var permissions = this._permissionRepository.GetAll();
            var query = from f in functions
                        join p in permissions on f.Id equals p.FunctionId
                        join r in this._roleManager.Roles on p.RoleId equals r.Id
                        where roles.Contains(r.Name) && f.Name == functionCode
                        select p;
            return query.AnyAsync();
        }
    }
}
