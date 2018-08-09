using Authentication.Applications;
using Authentication.Applications.ViewModel;
using Authentication.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Authentication.WebAPI.Controllers
{
    public class PersonController : ApiController
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }
        //Get api/Person
        /// <summary>
        /// This is get all data in database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            return new OkObjectResult(_personService.GetListPerson("D"));
        }
        /// <summary>
        /// This is Add and Update data
        /// </summary>
        /// <param name="personViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SaveEntity(PersonViewModel personViewModel)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(x => x.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            else
            {
                if (personViewModel.Id == Guid.Empty)
                {
                    _personService.Add(personViewModel);
                }
                else
                {
                    _personService.Update(personViewModel);
                }
                _personService.Save();
            }
            return new OkObjectResult(personViewModel);
        }
        /// <summary>
        /// This is get data with id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Person), 200)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        public IActionResult GetById(Guid id)
        {
            var model = _personService.GetById(id);
            return new OkObjectResult(model);
        }
        /// <summary>
        /// This is list data with paging
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortBy"></param>
        /// <returns></returns>
        [HttpGet("{keyword},{page},{pageSize},{sortBy}")]
        public IActionResult GetAllPaging(string keyword, int page, int pageSize, string sortBy)
        {
            var model = _personService.GetAllPaging(keyword, page, pageSize, sortBy);
            return new OkObjectResult(model);
        }
        /// <summary>
        /// This is delete data
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            else
            {
                _personService.Delete(id);
                _personService.Save();
            }
            return new OkObjectResult(id);
        }
    }
}