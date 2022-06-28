using NSUOW.Application.DTOs.Common;
using NSUOW.Application.Models.Pagination;
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

        Task<TEntity> AddAsync(TEntity entity);

        TEntity Add(TEntity entity);

        Task DeleteAsync(int id);

        void Delete(int id);

        Task<IReadOnlyList<TEntity>> GetAllAsync();

        IReadOnlyList<TEntity> GetAll();

        Task<TEntity?> GetByIdAsync(int id);

        TEntity? GetById(int id);

        Task UpdateAsync(TEntity entity);

        void Update(TEntity entity);

        Task<IReadOnlyList<TEntity>> QueryAsync(Expression<Func<TEntity, bool>>? predicate = null);
        IReadOnlyList<TEntity> Query(Expression<Func<TEntity, bool>>? predicate = null);

        Task<IReadOnlyList<TEntity>> QueryAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            params Expression<Func<TEntity, object>>[]? includes);

        IReadOnlyList<TEntity> Query(
           Expression<Func<TEntity, bool>>? predicate = null,
           params Expression<Func<TEntity, object>>[]? includes);

        Task<PagedResult<TDto>> QueryAsync(
            int page,
            int pageSize,
            Expression<Func<TEntity, bool>>? predicate = null);

        PagedResult<TDto> Query(
             int page,
             int pageSize,
             Expression<Func<TEntity, bool>>? predicate = null);

        Task<PagedResult<TDto>> QueryAsync(
            int page,
            int pageSize,
            Expression<Func<TEntity, bool>>? predicate = null,
            params Expression<Func<TEntity, object>>[]? includes);

        PagedResult<TDto> Query(
            int page,
            int pageSize,
            Expression<Func<TEntity, bool>>? predicate = null,
            params Expression<Func<TEntity, object>>[]? includes);

        Task<PagedResult<TDto>> QueryAsync(
            int page,
            int pageSize,
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            params Expression<Func<TEntity, object>>[] includes);

        PagedResult<TDto> Query(
             int page,
             int pageSize,
             Expression<Func<TEntity, bool>>? predicate = null,
             Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
             params Expression<Func<TEntity, object>>[] includes);

        Task<TEntity?> QueryFirstAsync(Expression<Func<TEntity, bool>>? predicate = null);

        TEntity? QueryFirst(Expression<Func<TEntity, bool>>? predicate = null);

        Task<TEntity?> QueryFirstAsync(Expression<Func<TEntity, bool>>? predicate = null,
            params Expression<Func<TEntity, object>>[]? includes);

        TEntity? QueryFirst(Expression<Func<TEntity, bool>>? predicate = null,
            params Expression<Func<TEntity, object>>[]? includes);

        Task<TEntity?> QueryFirstAsync(Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            params Expression<Func<TEntity, object>>[]? includes);

        TEntity? QueryFirst(Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            params Expression<Func<TEntity, object>>[]? includes);
    }
}
