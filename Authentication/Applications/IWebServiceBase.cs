using Authentication.Utils.Dtos;
using Authentication.Utils.Enum;
using Authentication.Utils.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Authentication.Applications
{
    public interface IWebServiceBase<TEntity, TPrimaryKey, TViewModel> where TViewModel : class
        where TEntity : DomainEntity<TPrimaryKey>
    {
        void Add(TViewModel viewModel);

        void Update(TViewModel viewModel);

        void Delete(TPrimaryKey id);

        TViewModel GetById(TPrimaryKey id);

        List<TViewModel> GetAll();

        PagedResult<TViewModel> GetAllPaging(Expression<Func<TEntity, bool>> predicate, Func<TEntity, bool> orderBy,
            SortDirection sortDirection, int pageIndex, int pageSize);

        void Save();
    }
}