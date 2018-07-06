using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        List<T> Get(
           Expression<Func<T, bool>> filter = null,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           int? skip = null, int? take = null,
           params Expression<Func<T, object>>[] includes
           );

        IQueryable<T> Query(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            int? skip = null, int? take = null);

        T GetById(object id);

        List<T> GetWithRawSql(string query, params object[] parameters);

        Task<List<T>> GetWithRawSqlAsync(string query, params object[] parameters);

        long Count(Expression<Func<T, bool>> filter);

        long Count();

        void Insert(T entity);
        Task<int> InsertAsync(T entity);
        void InsertIfNotExists<TKey>(T entity, Func<T, TKey> predicate);
        void InsertMany(List<T> entities);
        void Update(T entity);
        void Delete(T entity);
        void Delete(object id);
        void DeleteMany(List<T> entities);
    }
}
