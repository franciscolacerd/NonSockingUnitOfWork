using NSUOW.Application.Models.Persistence;
using NSUOW.Domain.Common;
using NSUOW.Persistence.Repositories.Common;
using System.Linq.Expressions;

namespace NSUOW.Persistence.Contracts
{
    public interface IGenericRepository<TEntity, TContext>
        where TEntity : BaseDomainEntity
        where TContext : BaseDbContext
    {

        Task<TEntity> AddAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);
        Task<IReadOnlyList<TEntity>> GetAllAsync();

        Task<TEntity?> GetByIdAsync(int id);

        Task<IReadOnlyList<TEntity>> QueryAsync(Expression<Func<TEntity, bool>>? predicate = null);

        Task<IReadOnlyList<TEntity>> QueryAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            params Expression<Func<TEntity, object>>[]? includes);

        Task<PagedList<TEntity>> QueryAsync(
            int page,
            int pageSize,
            Expression<Func<TEntity, bool>>? predicate = null);

        Task<PagedList<TEntity>> QueryAsync(
            int page,
            int pageSize,
            Expression<Func<TEntity, bool>>? predicate = null,
            params Expression<Func<TEntity, object>>[]? includes);

        Task<PagedList<TEntity>> QueryAsync(
           int page,
           int pageSize,
           string sortColumn,
           string sortDirection,
           Expression<Func<TEntity, bool>>? predicate = null);

        Task<PagedList<TEntity>> QueryAsync(
            int page,
            int pageSize,
            string sortColumn,
            string sortDirection,
            Expression<Func<TEntity, bool>>? predicate = null,
            params Expression<Func<TEntity, object>>[] includes);

        Task<TEntity?> QueryFirstAsync(Expression<Func<TEntity, bool>>? predicate = null);

        Task<TEntity?> QueryFirstAsync(Expression<Func<TEntity, bool>>? predicate = null,
            params Expression<Func<TEntity, object>>[]? includes);

        Task UpdateAsync(TEntity entity);
    }
}
