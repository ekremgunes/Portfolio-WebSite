using Portfolio.Common.Enums;
using Portfolio.Entities;
using System.Linq.Expressions;

namespace Portfolio.DataAccess.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task CreateAsync(T entity);
        void Remove(T entity);
        void RemoveRange(List<T> entities);
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter);
        Task<List<T>> GetAllAsync<TKey>(Expression<Func<T, TKey>> selector, OrderByType orderByType = OrderByType.DESC);
        Task<List<T>> GetAllAsync<TKey>(Expression<Func<T, bool>> filter, Expression<Func<T, TKey>> selector, OrderByType orderByType = OrderByType.DESC);
        void Update(T entity,T unchanged);
        Task<T> FindAsync(object id);
        IQueryable<T> GetQuery();
        Task<T> GetByFilterAsync(Expression<Func<T, bool>> filter, bool asNoTracking = false);
        Task<int> getCountAsync();
    }
}
