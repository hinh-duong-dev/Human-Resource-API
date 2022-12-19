using System;
using System.Linq.Expressions;

namespace HumanResourceAPI.Infrastructure
{
    public interface IRepositoryBase<T, K> where T : class
    {
        T FindById(K id, params Expression<Func<T, object>>[] includeProperties);

        Task<T> FindByIdAsync(K id, params Expression<Func<T, object>>[] includeProperties);

        T FindSingle(bool trackChange, Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);

        Task<T> FindSingleAsync(bool trackChange, Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);

        IQueryable<T> FindAll(bool trackChange, params Expression<Func<T, object>>[] includeProperties);

        IQueryable<T> FindAll(bool trackChange, Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);

        void Create(T entity);

        void Update(T entity);

        void Delete(T entity);

    }
}