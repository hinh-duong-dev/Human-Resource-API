using System.Linq.Expressions;
using Entities;
using HumanResourceAPI.Infrastructure;
using Microsoft.EntityFrameworkCore;
namespace Repository
{
    public class RepositoryBase<TEntity, K> : IRepositoryBase<TEntity, K>
        where TEntity : DomainEntity<TEntity>
    {
        private readonly AppDbContext _context;

        public RepositoryBase(AppDbContext context) 
        {
            _context = context;
        }

        public void Create(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public IQueryable<TEntity> FindAll(bool trackChange, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> items = _context.Set<TEntity>();

            if (!trackChange) items.AsNoTracking();

            if (includeProperties == null) return items;

            return includeProperties.Aggregate(items, (current, includeProperty) => current.Include(includeProperty));
        }

        public IQueryable<TEntity> FindAll(bool trackChange, Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> items = _context.Set<TEntity>();

            if (!trackChange) items.AsNoTracking();

            if (includeProperties == null) return items.Where(predicate);

            items = includeProperties.Aggregate(items, (current, includeProperty) => current.Include(includeProperty));

            return items.Where(predicate);
        }

        public TEntity FindById(K id, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return FindAll(trackChange: false, includeProperties).SingleOrDefault(x => x.Id.Equals(id));
        }

        public Task<TEntity> FindByIdAsync(K id, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return FindAll(trackChange: false, includeProperties).SingleOrDefaultAsync(x => x.Id.Equals(id));
        }

        public TEntity FindSingle(bool trackChange, Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return FindAll(trackChange, includeProperties).SingleOrDefault(x => x.Id.Equals(predicate));
        }

        public Task<TEntity> FindSingleAsync(bool trackChange, Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return FindAll(trackChange, includeProperties).SingleOrDefaultAsync(x => x.Id.Equals(predicate));
        }
    }
}