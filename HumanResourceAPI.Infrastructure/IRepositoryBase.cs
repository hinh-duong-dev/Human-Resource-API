using System.Linq.Expressions;

namespace HumanResourceAPI.Infrastructure
{
    public interface IRepositoryBase<T, K> where T : class
    {
        T FindById(K id, params Expression<Func<T, object>>[] includeProperties);

        Task<T> FindByIdAsync(K id, params Expression<Func<T, object>>[] includeProperties);

        Task<T> FindSingle(Expression<Func<T, bool>>[] predicate, params Expression<Func<T, object>>[] includeProperties);

        Task<T> FindSingleAsync(Expression<Func<T, bool>>[] predicate, params Expression<Func<T, object>>[] includeProperties);

        IQueryable<T> FindAll(params Expression<Func<T, object>>[] includeProperties);

        IQueryable<T> FindAll(Expression<Func<T, bool>>[] predicate, params Expression<Func<T, object>>[] includeProperties);

    }
}