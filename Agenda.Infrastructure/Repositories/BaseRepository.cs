using Agenda.Domain.Interfaces;
using Agenda.Domain.Models;
using Agenda.Infrastructure.Data;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Agenda.Domain.Core;

namespace Agenda.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : Register
    {
        protected readonly ApplicationContext _context;
        protected readonly DbSet<T> _dbSet;
        private IQueryable<T> _preQuery;

        public BaseRepository(ApplicationContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
            _preQuery = _dbSet.AsQueryable();
        }

        public virtual IQueryable<T> Query()
        {
            return _preQuery;
        }

        public virtual void AddPreQuery(Func<IQueryable<T>, IQueryable<T>> query)
        {
            _preQuery = query.Invoke(_preQuery);
        }

        protected virtual IQueryable<T> GetQueryable(
            int? skip = null,
            int? take = null,
            Expression<Func<T, bool>> filter = null,
            bool asNoTracking = true)
        {
            IQueryable<T> query = Query();
            if (filter is not null && filter.Parameters[0].Name != "f")
                query = query.Where(filter);

            if (asNoTracking)
                query = query.AsNoTracking();

            if (skip.HasValue)
                query = query.Skip((int)skip);

            if (take.HasValue)
                query = query.Take((int)take);

            return query;
        }

        public async Task<T> AddAsync(T entity)
        {
            entity.CreatedAt = DateTime.Now;
            var query = _dbSet.Add(entity);
            return await Task.FromResult(query.Entity);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            entity.UpdatedAt = DateTime.Now;
            var query = _dbSet.Update(entity);
            return await Task.FromResult(query.Entity);
        }

        public async Task<T> DeleteAsync(int id)
        {
            var contactToBeRemoved = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);

            if (contactToBeRemoved == null)
                throw new InvalidOperationException($"Id: {id} to remove {typeof(Contact).Name} is invalid");

            var query = _dbSet.Remove(contactToBeRemoved);

            return await Task.FromResult(query.Entity);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<T>> GetAllAsync(
            int? skip = null,
            int? take = null,
            Expression<Func<T, bool>> filter = null,
            bool asNoTracking = true)
        {
            return await GetQueryable(skip, take, filter, asNoTracking).ToListAsync();
        }

        public async Task<int> CountAsync(
            Expression<Func<T, bool>> filter = null,
            bool asNoTracking = true
        )
        {
            return (await GetQueryable(null, null, filter, asNoTracking).ToListAsync()).Count();
        }
    }
}
