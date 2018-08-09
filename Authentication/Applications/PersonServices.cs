using Authentication.Applications.ViewModel;
using Authentication.Entity;
using Authentication.Entity.EF;
using Authentication.Utils.Dtos;
using Authentication.Utils.Enum;
using AutoMapper.QueryableExtensions;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Authentication.Applications
{
    public class PersonServices : WebServiceBase<Person, Guid, PersonViewModel>, IPersonService
    {
        private IRepository<Person, Guid> _personRepository;


        public PagedResult<PersonViewModel> GetAllPaging(string keyword, int page, int pageSize, string sortBy)
        {
            var query = _personRepository.GetAll().Where(c => c.Status == Status.Active);
            if (!string.IsNullOrEmpty(keyword))
                query = query.Where(x => x.Name.Contains(keyword) || x.Code.Contains(keyword));
            int totalRow = query.Count();
            switch (sortBy)
            {
                case "name":
                    query = query.OrderBy(x => x.Name);
                    break;

                case "lastest":
                    query = query.OrderByDescending(x => x.CreatedDate);
                    break;

                default:
                    query = query.OrderByDescending(x => x.CreatedDate);
                    break;
            }
            query = query.Skip((page - 1) * pageSize)
                .Take(pageSize);

            var data = query.ProjectTo<PersonViewModel>().ToList();
            var paginationSet = new PagedResult<PersonViewModel>()
            {
                Results = data,
                CurrentPage = page,
                RowCount = totalRow,
                PageSize = pageSize
            };

            return paginationSet;
        }

        public List<PersonViewModel> GetListPerson(string keyword)
        {
            IQueryable<PersonViewModel> query;
            if (!string.IsNullOrEmpty(keyword))
                query = _personRepository.GetAll().Where(x => x.Name.Contains(keyword)).ProjectTo<PersonViewModel>();
            else
                query = _personRepository.GetAll().ProjectTo<PersonViewModel>();
            return query.ToList();
        }

        public void ImportExcel(string filePath)
        {
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets[1];
                Person person;
                for (int i = workSheet.Dimension.Start.Row + 1; i <= workSheet.Dimension.End.Row; i++)
                {
                    person = new Person();
                    person.Name = workSheet.Cells[i, 1].Value.ToString();
                    person.Code = workSheet.Cells[i, 2].Value.ToString();
                    person.Job = workSheet.Cells[i, 3].Value.ToString();
                    DateTime.TryParse(workSheet.Cells[i, 4].Value.ToString(), out var dateOfBirth);
                    person.DateOfBirth = dateOfBirth;
                    person.Status = Status.Active;
                    _personRepository.Insert(person);
                }
            }
        }

        public PersonServices(IUnitOfWork unitOfWork, IRepository<Person, Guid> personRepository) : base(personRepository, unitOfWork)
        {
            _personRepository = personRepository;
        }
    }
}