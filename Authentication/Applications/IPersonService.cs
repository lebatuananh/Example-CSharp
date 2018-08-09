using Authentication.Applications.ViewModel;
using Authentication.Entity;
using Authentication.Utils.Dtos;
using System;
using System.Collections.Generic;

namespace Authentication.Applications
{
    public interface IPersonService : IWebServiceBase<Person, Guid, PersonViewModel>
    {
        PagedResult<PersonViewModel> GetAllPaging(string keyword, int page, int pageSize, string sortBy);

        List<PersonViewModel> GetListPerson(string keyword);

        void ImportExcel(string filePath);
    }
}