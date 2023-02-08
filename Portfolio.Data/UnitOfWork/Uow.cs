using Portfolio.DataAccess.Context;
using Portfolio.DataAccess.Interfaces;
using Portfolio.DataAccess.Repositories;
using Portfolio.Entities;

namespace Portfolio.DataAccess.UnitOfWork
{
    public class Uow : IUow 
    {
        private readonly PortfolioContext _context;

        public Uow(PortfolioContext context)
        {
            _context = context;
        }

        public IRepository<T> GetRepository<T>() where T : BaseEntity
        {
            return new Repository<T>(_context);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
