using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VideoAppDAL.Entities;

namespace VideoAppDAL.Interfaces
{
    public interface IRepository<T> where T:BaseEntity
    {
        Task Create(T item);
        void Update(T item);
        Task Delete(int id);
        Task<IEnumerable<T>> GetAll(params Expression<Func<T,object>>[] includes);
        Task<T> Find(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task<T> FindAsNoTracking(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task<List<T>> FindMany(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task<T> Get(int id, params Expression<Func<T, object>>[] includes);
    }
}
