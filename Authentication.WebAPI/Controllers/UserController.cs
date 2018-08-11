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

    /// <summary>
    /// The user controller.
    /// </summary>
    public class UserController : ApiController
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="userService">
        /// The user service.
        /// </param>
        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        /// <summary>
        /// This is getall user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            var model = this._userService.GetAll();
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
            var model = await _userService.GetById(id);

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
            var model = this._userService.GetAllPagingAsync(keyword, page, pageSize);
            return new OkObjectResult(model);
        }

        /// <summary>
        /// The save entity.
        /// </summary>
        /// <param name="appUserViewModel">
        /// The app user view model.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> SaveEntity(AppUserViewModel appUserViewModel)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            else
            {
                if (appUserViewModel.Id == Guid.Empty)
                {
                    await this._userService.AddAsync(appUserViewModel);
                }
                else
                {
                    await this._userService.UpdateAsync(appUserViewModel);
                }
                return new OkObjectResult(appUserViewModel);

            }

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
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            else
            {
                await this._userService.DeleteAsync(id);
                return new OkObjectResult(id);

            }
        }

    }
}