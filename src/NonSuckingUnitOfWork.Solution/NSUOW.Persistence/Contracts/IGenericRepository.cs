using NSUOW.Application.DTOs.Common;
using NSUOW.Application.Models.Persistence;
using NSUOW.Domain.Common;
using NSUOW.Persistence.Repositories.Common;
using System.Linq.Expressions;

namespace NSUOW.Persistence.Contracts
{
    public interface IGenericRepository<TEntity, TDto, TContext>
        where TEntity : BaseDomainEntity
        where TDto : BaseDto
        where TContext : BaseDbContext
    {

        //Entity and not dto because we need identity value after unit of work SaveChangesAsync()
        Task<TEntity> AddAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);
        Task<IReadOnlyList<TDto>> GetAllAsync();

        Task<TDto?> GetByIdAsync(int id);

        Task<IReadOnlyList<TDto>> QueryAsync(Expression<Func<TEntity, bool>>? predicate = null);

        Task<IReadOnlyList<TDto>> QueryAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            params Expression<Func<TEntity, object>>[]? includes);

        Task<PagedList<TDto>> QueryAsync(
            int page,
            int pageSize,
            Expression<Func<TEntity, bool>>? predicate = null);

        Task<PagedList<TDto>> QueryAsync(
            int page,
            int pageSize,
            Expression<Func<TEntity, bool>>? predicate = null,
            params Expression<Func<TEntity, object>>[]? includes);

        Task<PagedList<TDto>> QueryAsync(
           int page,
           int pageSize,
           string sortColumn,
           string sortDirection,
           Expression<Func<TEntity, bool>>? predicate = null);

        Task<PagedList<TDto>> QueryAsync(
            int page,
            int pageSize,
            string sortColumn,
            string sortDirection,
            Expression<Func<TEntity, bool>>? predicate = null,
            params Expression<Func<TEntity, object>>[] includes);

        Task<TDto?> QueryFirstAsync(Expression<Func<TEntity, bool>>? predicate = null);

        Task<TDto?> QueryFirstAsync(Expression<Func<TEntity, bool>>? predicate = null,
            params Expression<Func<TEntity, object>>[]? includes);

        Task UpdateAsync(TEntity entity);
    }
}
