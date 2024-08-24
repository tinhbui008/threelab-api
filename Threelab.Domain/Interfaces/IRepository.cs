using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Threelab.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        DbSet<T> Entities { get; }
        DbContext Context { get; }
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAsyncByFilter(Expression<Func<T, bool>> filter = null);
        Task<T> GetOneByFilter(Expression<Func<T, bool>> filter = null);
        Task<T> FindAsync(params object[] keyValues);
        Task InsertAsync(T entity, bool saveChanges = true);
        Task InsertRangeAsync(IEnumerable<T> entities, bool saveChanges = true);
        Task DeleteAsync(int id, bool saveChanges = true);
        Task DeleteAsync(T entity, bool saveChanges = true);
        Task DeleteRangeAsync(IEnumerable<T> entities, bool saveChanges = true);
        Task Update(T entity, bool saveChanges = true);
    }
}