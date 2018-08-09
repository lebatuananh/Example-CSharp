using Authentication.Entity.EF;
using Authentication.Utils.Dtos;
using Authentication.Utils.Enum;
using Authentication.Utils.SharedKernel;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Authentication.Applications
{
    public abstract class WebServiceBase<TEntity, TPrimaryKey, TViewModel> : IWebServiceBase<TEntity, TPrimaryKey, TViewModel>
         where TViewModel : class
         where TEntity : DomainEntity<TPrimaryKey>
    {
        private readonly IRepository<TEntity, TPrimaryKey> _repository;
        private readonly IUnitOfWork _unitOfWork;

        protected WebServiceBase(IRepository<TEntity, TPrimaryKey> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public virtual void Add(TViewModel viewModel)
        {
            var model = Mapper.Map<TViewModel, TEntity>(viewModel);
            _repository.Insert(model);
        }

        public virtual void Delete(TPrimaryKey id)
        {
            _repository.Delete(id);
        }

        public virtual TViewModel GetById(TPrimaryKey id)
        {
            return Mapper.Map<TEntity, TViewModel>(_repository.GetById(id));
        }

        public virtual List<TViewModel> GetAll()
        {
            return _repository.GetAll().ProjectTo<TViewModel>().ToList();
        }

        public virtual PagedResult<TViewModel> GetAllPaging(Expression<Func<TEntity, bool>> predicate, Func<TEntity, bool> orderBy,
            SortDirection sortDirection, int pageIndex, int pageSize)
        {
            var query = _repository.GetAll().Where(predicate);

            int totalRow = query.Count();

            if (sortDirection == SortDirection.Ascending)
            {
                query = query.OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsQueryable();
            }
            else
            {
                query = query.OrderByDescending(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsQueryable();
            }

            var data = query.ProjectTo<TViewModel>().ToList();
            var paginationSet = new PagedResult<TViewModel>()
            {
                Results = data,
                CurrentPage = pageIndex,
                RowCount = totalRow,
                PageSize = pageSize
            };

            return paginationSet;
        }

        public virtual void Save()
        {
            _unitOfWork.Commit();
        }

        public virtual void Update(TViewModel viewModel)
        {
            var model = Mapper.Map<TViewModel, TEntity>(viewModel);
            _repository.Update(model);
        }
    }
}