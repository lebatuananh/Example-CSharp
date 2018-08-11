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

    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;


        public UserService(UserManager<AppUser> userManager)
        {
            this._userManager = userManager;
        }

        public async Task<bool> AddAsync(AppUserViewModel userVm)
        {
            var user = new AppUser()
            {
                UserName = userVm.UserName,
                Avatar = userVm.Avatar,
                Email = userVm.Email,
                FullName = userVm.FullName,
                DateCreated = DateTime.Now,
                PhoneNumber = userVm.PhoneNumber
            };
            var result = await this._userManager.CreateAsync(user, userVm.Password);
            if (result.Succeeded && userVm.Roles.Count > 0)
            {
                var appUser = await this._userManager.FindByNameAsync(user.UserName);
                if (appUser != null)
                    await this._userManager.AddToRolesAsync(appUser, userVm.Roles);
            }
            return true;
        }

        public async Task DeleteAsync(Guid id)
        {
            var user = await this._userManager.FindByIdAsync(id.ToString());
            await this._userManager.DeleteAsync(user);
        }

        //public async Task<List<AppUserViewModel>> GetAllAsync()
        //{
        //    var user = await this._userManager.Users.ProjectTo<AppUserViewModel>().ToListAsync();
        //    return user;
        //}
        public List<AppUserViewModel> GetAll()
        {
            var user = this._userManager.Users.ProjectTo<AppUserViewModel>().ToList();
            return user;
        }

        public async Task<AppUserViewModel> GetById(Guid id)
        {
            var user = await this._userManager.FindByIdAsync(id.ToString());
            var roles = await this._userManager.GetRolesAsync(user);
            var userVm = Mapper.Map<AppUser, AppUserViewModel>(user);
            userVm.Roles = roles;
            return userVm;
        }

        public async Task UpdateAsync(AppUserViewModel userVm)
        {
            var user = await this._userManager.FindByIdAsync(userVm.Id.ToString());
            //Remove current roles in db
            var currentRoles = await this._userManager.GetRolesAsync(user);

            var result = await this._userManager.AddToRolesAsync(user,
                             userVm.Roles.Except(currentRoles).ToArray());

            if (result.Succeeded)
            {
                string[] needRemoveRoles = currentRoles.Except(userVm.Roles).ToArray();
                await this._userManager.RemoveFromRolesAsync(user, needRemoveRoles);

                //Update user detail
                user.FullName = userVm.FullName;
                user.Email = userVm.Email;
                user.BirthDay = userVm.DateOfBirth;
                user.DateModified = userVm.ModifiedDate;
                user.PhoneNumber = userVm.PhoneNumber;
                await this._userManager.UpdateAsync(user);
            }
        }
    }
}
