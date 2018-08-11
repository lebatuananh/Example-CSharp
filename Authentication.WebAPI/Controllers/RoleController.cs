using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.WebAPI.Controllers
{
    using Authentication.Applications;
    using Authentication.Applications.ViewModel;

    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class RoleController : ApiController
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            this._roleService = roleService;
        }

        /// <summary>
        /// The get all.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var model = await this._roleService.GetAllAsync();
            return new OkObjectResult(model);
        }

        /// <summary>
        /// The get by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var model = await this._roleService.GetByIdAsync(id);
            return new OkObjectResult(model);
        }

        /// <summary>
        /// The get all paging.
        /// </summary>
        /// <param name="keyword">
        /// The keyword.
        /// </param>
        /// <param name="page">
        /// The page.
        /// </param>
        /// <param name="pageSize">
        /// The page size.
        /// </param>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        [HttpGet("{keyword},{page},{pageSize}")]
        public IActionResult GetAllPaging(string keyword, int page, int pageSize)
        {
            var model = this._roleService.GetAllPagingAsync(keyword, page, pageSize);
            return new OkObjectResult(model);
        }

        /// <summary>
        /// The save entity.
        /// </summary>
        /// <param name="roleViewModel">
        /// The role view model.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> SaveEntity(AppRoleViewModel roleViewModel)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(x => x.Errors);
                return new BadRequestObjectResult(allErrors);
            }

            if (roleViewModel.Id==Guid.Empty)
            {
                await this._roleService.AddAsync(roleViewModel);
            }
            else
            {
                await this._roleService.UpdateAsync(roleViewModel);
            }
            return new OkObjectResult(roleViewModel);
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [HttpPost("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await this._roleService.DeleteAsync(id);
            return new OkObjectResult(id);
        }

        /// <summary>
        /// The save permission.
        /// </summary>
        /// <param name="permissionViewModels">
        /// The permission view models.
        /// </param>
        /// <param name="roleId">
        /// The role id.
        /// </param>
        /// <returns>
        /// The <see cref="IActionResult"/>.
        /// </returns>
        [HttpPost("{roleId}")]
        public IActionResult SavePermission(List<PermissionViewModel> permissionViewModels, Guid roleId)
        {
            this._roleService.SavePermission(permissionViewModels,roleId);
            return new OkResult();
        }
    }
}