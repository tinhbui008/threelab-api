using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Threelab.Domain.Interfaces;

namespace Threelab.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public DbSet<T> Entities => Context.Set<T>();

        public DbContext Context { get; private set; }

        public Repository(DbContext context)
        {
            Context = context;
        }

        public async Task DeleteAsync(int id, bool saveChanges = true)
        {
            var user = await Entities.FindAsync(id);
        }

        public async Task DeleteAsync(T entity, bool saveChanges = true)
        {
            Entities.Remove(entity);
            if (saveChanges)
            {
                await Context.SaveChangesAsync();
            }
        }

        public async Task DeleteRangeAsync(IEnumerable<T> entities, bool saveChanges = true)
        {
            var enumerable = entities as T[] ?? entities.ToArray();
            if (enumerable.Any())
            {
                Entities.RemoveRange(enumerable);
            }

            if (saveChanges)
            {
                await Context.SaveChangesAsync();
            }
        }

        public async Task<T> FindAsync(params object[] keyValues)
        {
            return await Entities.FindAsync(keyValues);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Entities.ToListAsync();
        }

        public async Task InsertAsync(T entity, bool saveChanges = true)
        {
            await Entities.AddAsync(entity);
            //if (saveChanges)
            //{
            //    await Context.SaveChangesAsync();
            //}
        }

        public async Task InsertRangeAsync(IEnumerable<T> entities, bool saveChanges = true)
        {
            await Entities.AddRangeAsync(entities);

            if (saveChanges)
            {
                await Context.SaveChangesAsync();
            }
        }

        public async Task Update(T entity, bool saveChanges = true)
        {
            Entities.Update(entity);
            if (saveChanges)
            {
                await Context.SaveChangesAsync();
            }
        }

        public async Task<T> GetOneByFilter(Expression<Func<T, bool>> filter = null)
        {
            return await Entities.Where(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAsyncByFilter(Expression<Func<T, bool>> filter = null)
        {
            return await Entities.Where(filter).ToListAsync();
        }
    }
}