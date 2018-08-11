namespace IdentityFrameworkExample.Application.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;

    using global::AutoMapper;
    using global::AutoMapper.QueryableExtensions;

    using IdentityFrameworkExample.Application.Interface;
    using IdentityFrameworkExample.Application.ViewModel;
    using IdentityFrameworkExample.Entity;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// The role service.
    /// </summary>
    public class RoleService : IRoleService
    {
        private RoleManager<AppRole> _roleManager;

        public RoleService(RoleManager<AppRole> roleManager)
        {
            this._roleManager = roleManager;
        }

        public async Task<bool> AddAsync(AppRoleViewModel roleViewModel)
        {
            var role = new AppRole()
            {
                Name = roleViewModel.Name,
                Description = roleViewModel.Description
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
            await this._roleManager.UpdateAsync(role);
        }
    }
}
