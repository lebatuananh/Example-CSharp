﻿using Authentication.Utils.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Authentication.Entity.EF
{
    public interface IRepository<TEntity, TPrimaryKey> where TEntity : DomainEntity<TPrimaryKey>
    {
        int Count();

        void Delete(TEntity entity);

        void Delete(TPrimaryKey id);

        void Delete(Expression<Func<TEntity, bool>> predicate);

        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        TEntity FirstOrDefault(TPrimaryKey id);

        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> FirstOrDefaultAsync(TPrimaryKey id);

        TEntity GetById(TPrimaryKey id);

        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate, bool isAll = true);

        IQueryable<TEntity> GetAll(bool isAll = true);

        IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors);

        List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate);

        List<TEntity> GetAllList();

        Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate);

        Task<List<TEntity>> GetAllListAsync();

        Task<TEntity> GetAsync(TPrimaryKey id);

        TEntity Insert(TEntity entity);

        TPrimaryKey InsertAndGetId(TEntity entity);

        Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity);

        Task<TEntity> InsertAsync(TEntity entity);

        long LongCount(Expression<Func<TEntity, bool>> predicate);

        long LongCount();

        Task<long> LongCountAsync();

        Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate);

        TEntity Single(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate);

        TEntity Update(TEntity entity);
    }
}