using Authentication.Entity.Interface;
using Authentication.Utils.Extensions;
using Authentication.Utils.SharedKernel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Authentication.Entity.EF
{
    public class EFRepository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>, IDisposable where TEntity : DomainEntity<TPrimaryKey>
    {
        protected readonly AppDbContext _context;

        public EFRepository(AppDbContext context)
        {
            _context = context;
        }

        public int Count()
        {
            return GetAll().Count();
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public void Delete(TPrimaryKey id)
        {
            _context.Set<TEntity>().Remove(GetById(id));
        }

        public void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            _context.Set<TEntity>().RemoveRange(GetAll().Where(predicate));
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().FirstOrDefault(predicate);
        }

        public TEntity FirstOrDefault(TPrimaryKey id)
        {
            return _context.Set<TEntity>().FirstOrDefault(x => x.Id.Equals(id));
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().FirstOrDefaultAsync(predicate);
        }

        public async Task<TEntity> FirstOrDefaultAsync(TPrimaryKey id)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public TEntity GetById(TPrimaryKey id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate, bool isAll = true)
        {
            return GetAll(isAll).Where(predicate);
        }

        public IQueryable<TEntity> GetAll(bool isAll = true)
        {
            if (typeof(TEntity) is IHasSoftDelete && isAll == false)
            {
                return _context.Set<TEntity>().Where(x => ((IHasSoftDelete)x).IsDelete == false).AsQueryable();
            }
            return _context.Set<TEntity>().AsQueryable();
        }

        public IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            IQueryable<TEntity> items = GetAll();

            if (propertySelectors != null)
            {
                foreach (var includeProperty in propertySelectors)
                {
                    items = items.Include(includeProperty);
                }
            }
            return items;
        }

        public List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Where(predicate).ToList();
        }

        public List<TEntity> GetAllList()
        {
            return GetAll().ToList();
        }

        public async Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().Where(predicate).ToListAsync();
        }

        public async Task<List<TEntity>> GetAllListAsync()
        {
            return await GetAll().ToListAsync();
        }

        public async Task<TEntity> GetAsync(TPrimaryKey id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public TEntity Insert(TEntity entity)
        {
            return _context.Set<TEntity>().Add(entity).Entity;
        }

        public TPrimaryKey InsertAndGetId(TEntity entity)
        {
            var result = _context.Set<TEntity>().Add(entity);
            return result.Entity.Id;
        }

        public async Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity)
        {
            var result = await _context.Set<TEntity>().AddAsync(entity);
            return result.Entity.Id;
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            var result = await _context.Set<TEntity>().AddAsync(entity);
            return result.Entity;
        }

        public long LongCount(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().LongCount(predicate);
        }

        public long LongCount()
        {
            return GetAll().LongCount();
        }

        public async Task<long> LongCountAsync()
        {
            return await GetAll().LongCountAsync();
        }

        public async Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().LongCountAsync(predicate);
        }

        public TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Single(predicate);
        }

        public async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().SingleAsync(predicate);
        }

        public TEntity Update(TEntity entity)
        {
            var dbEntity = _context.Set<TEntity>().AsNoTracking().Single(p => p.Id.Equals(entity.Id));
            var databaseEntry = _context.Entry(dbEntity);
            var inputEntry = _context.Entry(entity);

            //no items mentioned, so find out the updated entries
            IEnumerable<string> dateProperties = typeof(IDateTracking).GetPublicProperties().Select(x => x.Name);

            var allProperties = databaseEntry.Metadata.GetProperties()
                .Where(x => !dateProperties.Contains(x.Name));

            foreach (var property in allProperties)
            {
                var proposedValue = inputEntry.Property(property.Name).CurrentValue;
                var originalValue = databaseEntry.Property(property.Name).OriginalValue;

                if (proposedValue != null && !proposedValue.Equals(originalValue))
                {
                    databaseEntry.Property(property.Name).IsModified = true;
                    databaseEntry.Property(property.Name).CurrentValue = proposedValue;
                }
            }

            var result = _context.Set<TEntity>().Update(dbEntity);
            return result.Entity;
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}