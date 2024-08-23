using Microsoft.EntityFrameworkCore;

namespace Threelab.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        DbContext DbContext { get; }
        IRepository<T> Repository<T>() where T : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task BeginTransaction();
        Task CommitTransaction();
        Task RollbackTransaction();
    }
}