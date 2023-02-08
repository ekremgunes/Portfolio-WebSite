using Portfolio.DataAccess.Interfaces;
using Portfolio.Entities;

namespace Portfolio.DataAccess.UnitOfWork
{
    public interface IUow
    {
        IRepository<T> GetRepository<T>() where T: BaseEntity;
        Task SaveChangesAsync();
    }
}
