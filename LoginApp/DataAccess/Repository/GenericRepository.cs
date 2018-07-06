using DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        #region Fields

        public LoginAppDbContext _loginAppDbContext;

        public DbSet<T> _dbSet;

        #endregion

        #region Constructor

        public GenericRepository(LoginAppDbContext loginAppDbContext)
        {
            _loginAppDbContext = loginAppDbContext;
            _dbSet = loginAppDbContext.Set<T>();
        }

        #endregion

        #region Methods

        public virtual long Count(Expression<Func<T, bool>> filter)
        {
            return _dbSet.Where(filter).Count();
        }

        public virtual long Count()
        {
            return _dbSet.Count();
        }

        public virtual void Delete(T entity)
        {
            if (_loginAppDbContext.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }

        public virtual void Delete(object id)
        {
            T entity = _dbSet.Find(id);
            if (_loginAppDbContext.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            Delete(entity);
        }

        public virtual void DeleteMany(List<T> entities)
        {
            foreach (var item in entities)
            {
                if (_loginAppDbContext.Entry(item).State == EntityState.Detached)
                {
                    _dbSet.Attach(item);
                }
            }
            _dbSet.RemoveRange(entities);
        }

        public virtual List<T> Get( Expression<Func<T, bool>> filter = null, 
                            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, 
                            int? skip = null, 
                            int? take = null, 
                            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet.AsNoTracking();
            foreach (Expression<Func<T, object>> include in includes)
            {
                query = query.AsNoTracking().Include(include);
            }
            if (filter != null)
            {
                query = query.AsNoTracking().Where(filter);
            }
            if (orderBy != null)
            {
                query = orderBy(query).AsNoTracking();
            }
            if (skip.HasValue)
            {
                query = query.Skip(skip.Value).AsNoTracking();
            }
            if (take.HasValue)
            {
                query = query.Take(take.Value).AsNoTracking();
            }
            return query.AsNoTracking().ToList();
        }

        public virtual T GetById(object id)
        {
            return _dbSet.Find(id);
        }

        public List<T> GetWithRawSql(string query, params object[] parameters)
        {
            return _dbSet.FromSql(query).ToList();
        }

        public Task<List<T>> GetWithRawSqlAsync(string query, params object[] parameters)
        {
            return _dbSet.FromSql(query).ToListAsync();
        }

        public void Insert(T entity)
        {
            _dbSet.Add(entity);
        }

        public async Task<int> InsertAsync(T entity)
        {
            var res = await _dbSet.AddAsync(entity);
            return await Task.FromResult(1);
        }

        public void InsertIfNotExists<TKey>(T entity, Func<T, TKey> predicate)
        {
            var exists = _dbSet.Any(c => predicate(entity).Equals(predicate(c)));
            if (!exists)
            {
                _dbSet.Add(entity);
            }
        }

        public void InsertMany(List<T> entities)
        {
            _dbSet.AddRange(entities);
        }

        public IQueryable<T> Query(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int? skip = null, int? take = null)
        {
            IQueryable<T> query = _dbSet.AsNoTracking();
            if (filter != null)
            {
                query = query.Where(filter).AsNoTracking();
            }
            if (orderBy != null)
            {
                query = orderBy(query).AsNoTracking();
            }
            if (skip.HasValue)
            {
                query = query.Skip(skip.Value).AsNoTracking();
            }
            if (take.HasValue)
            {
                query = query.Take(take.Value).AsNoTracking();
            }
            return query.AsNoTracking();
        }

        public void Update(T entity)
        {

            _dbSet.Attach(entity);
            
            _loginAppDbContext.Entry(entity).State = EntityState.Modified;
            
            
        }


        #endregion
    }
}
