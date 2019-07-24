namespace Vimata.Data.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Vimata.Data.Models;

    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly VimataDbContext Context;

        public Repository(VimataDbContext context)
        {
            Context = context;
        }

        public Task<T> GetByIdAsync(int id) => Context.Set<T>().FindAsync(id);

        public Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
            => Context.Set<T>().FirstOrDefaultAsync(predicate);

        public async Task AddAsync(T entity)
        {
            // await Context.AddAsync(entity);
            entity.CreatedDate = DateTime.UtcNow;
            await Context.Set<T>().AddAsync(entity);
            await Context.SaveChangesAsync();
        }

        public Task UpdateAsync(T entity)
        {
            // In case AsNoTracking is used
            entity.ModifiedDate = DateTime.UtcNow;
            Context.Entry(entity).State = EntityState.Modified;
            return Context.SaveChangesAsync();
        }

        public Task RemoveAsync(T entity)
        {
            Context.Set<T>().Remove(entity);
            return Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> predicate)
        {
            return await Context.Set<T>().Where(predicate).ToListAsync();
        }

        public Task<int> CountAllAsync() => Context.Set<T>().CountAsync();

        public Task<int> CountWhereAsync(Expression<Func<T, bool>> predicate)
            => Context.Set<T>().CountAsync(predicate);
    }
}
