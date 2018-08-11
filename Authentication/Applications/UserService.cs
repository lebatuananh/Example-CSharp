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

    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;

        public readonly IUnitOfWork _uniOfWork;


        public UserService(UserManager<AppUser> userManager, IUnitOfWork uniOfWork)
        {
            this._userManager = userManager;
            this._uniOfWork = uniOfWork;
        }

        public async Task<bool> AddAsync(AppUserViewModel userVm)
        {
            var user = new AppUser()
            {
                UserName = userVm.UserName,
                Avatar = userVm.Avatar,
                Email = userVm.Email,
                FullName = userVm.FullName,
                CreatedDate = DateTime.Now,
                PhoneNumber = userVm.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user, userVm.Password);
            if (result.Succeeded && userVm.Roles.Count > 0)
            {
                var appUser = await _userManager.FindByNameAsync(user.UserName);
                if (appUser != null)
                    await _userManager.AddToRolesAsync(appUser, userVm.Roles);
            }
            return true;
        }

        public async Task DeleteAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            await _userManager.DeleteAsync(user);
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

        public PagedResult<AppUserViewModel> GetAllPagingAsync(string keyword, int page, int pageSize)
        {
            var query = _userManager.Users;
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => x.FullName.Contains(keyword)
                                         || x.UserName.Contains(keyword)
                                         || x.Email.Contains(keyword));

            int totalRow = query.Count();
            query = query.Skip((page - 1) * pageSize)
                .Take(pageSize);

            var data = query.Select(x => new AppUserViewModel()
            {
                UserName = x.UserName,
                Avatar = x.Avatar,
                DateOfBirth = x.DateOfBirth,
                Email = x.Email,
                FullName = x.FullName,
                Id = x.Id,
                PhoneNumber = x.PhoneNumber,
                Status = x.Status,
                DateCreated = x.CreatedDate
            }).ToList();
            var paginationSet = new PagedResult<AppUserViewModel>()
            {
                Results = data,
                CurrentPage = page,
                RowCount = totalRow,
                PageSize = pageSize
            };

            return paginationSet;
        }

        public async Task<AppUserViewModel> GetById(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            var roles = await _userManager.GetRolesAsync(user);
            var userVm = Mapper.Map<AppUser, AppUserViewModel>(user);
            userVm.Roles = roles;
            return userVm;
        }

        public void Save()
        {
            this._uniOfWork.Commit();
        }

        public async Task UpdateAsync(AppUserViewModel userVm)
        {
            var user = await _userManager.FindByIdAsync(userVm.Id.ToString());
            //Remove current roles in db
            var currentRoles = await _userManager.GetRolesAsync(user);

            var result = await _userManager.AddToRolesAsync(user,
                             userVm.Roles.Except(currentRoles).ToArray());

            if (result.Succeeded)
            {
                string[] needRemoveRoles = currentRoles.Except(userVm.Roles).ToArray();
                await _userManager.RemoveFromRolesAsync(user, needRemoveRoles);

                //Update user detail
                user.FullName = userVm.FullName;
                user.Status = userVm.Status;
                user.Email = userVm.Email;
                user.UserName = userVm.UserName;
                user.DateOfBirth = userVm.DateOfBirth;
                user.ModifiedDate = DateTime.Now;
                user.PhoneNumber = userVm.PhoneNumber;
                await _userManager.UpdateAsync(user);
            }
        }
    }
}
