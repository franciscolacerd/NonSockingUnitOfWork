using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NSUOW.Application.Extensions;
using NSUOW.Application.Models.Persistence;
using NSUOW.Domain.Common;
using NSUOW.Persistence.Contracts;
using NSUOW.Persistence.Repositories.Common;
using System.Linq.Expressions;

namespace NSUOW.Persistence.Repositories
{
    public class GenericRepository<TEntity, TContext> : BaseRepository<TEntity, TContext>, IGenericRepository<TEntity, TContext>
        where TEntity : BaseDomainEntity
        where TContext : BaseDbContext
    {
        private readonly TContext _dbContext;
        protected IMapper _mapper { get; }

        public GenericRepository(TContext dbContext, IMapper mapper) : base(dbContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var entityEntry = await _dbContext
                .Set<TEntity>()
                .AddAsync(entity);

            return entityEntry.Entity;
        }

        public async Task DeleteAsync(TEntity entity)
        {
            var dbSet = _dbContext.Set<TEntity>();

            var entitytoDelete = await dbSet.FindAsync(new object[] { entity.Id });

            if (entitytoDelete == null)
                return;

            dbSet.Remove(entity);
        }

        public async Task<IReadOnlyList<TEntity>> GetAllAsync()
        {
            return await _dbContext
                .Set<TEntity>()
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<TEntity?> GetByIdAsync(int id)
        {
            var key = _dbContext.Model.FindEntityType(typeof(TEntity))?.FindPrimaryKey()?.Properties.Single().Name;

            if (key == null) { return null; }

            return await _dbContext
                .Set<TEntity>()
                .AsNoTracking()
                .Where(x => id.Equals(EF.Property<long>(x, key)))
                .FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<TEntity>> QueryAsync(Expression<Func<TEntity, bool>>? predicate = null)
        {
            return await QueryAsync(predicate, null);
        }

        public async Task<IReadOnlyList<TEntity>> QueryAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            params Expression<Func<TEntity, object>>[]? includes)
        {
            var _query = SetFiltersToQuery(predicate, includes);

            return await _query
                 .ToListAsync();
        }

        public async Task<PagedList<TEntity>> QueryAsync(
            int page,
            int pageSize,
            Expression<Func<TEntity, bool>>? predicate = null)
        {
            return await QueryAsync(page, pageSize, predicate, null);
        }

        public async Task<PagedList<TEntity>> QueryAsync(
            int page,
            int pageSize,
            Expression<Func<TEntity, bool>>? predicate = null,
            params Expression<Func<TEntity, object>>[]? includes)
        {
            var _query = SetFiltersToQuery(predicate, includes);

            var count = await _query.CountAsync();

            var result = await _query
                 .Skip(Skip(page, pageSize))
                 .Take(pageSize)
                 .ToListAsync();

            return new PagedList<TEntity>(count, page, pageSize, null!, null!, result);
        }

        public async Task<PagedList<TEntity>> QueryAsync(
            int page,
            int pageSize,
            string sortColumn,
            string sortDirection,
            Expression<Func<TEntity, bool>>? predicate = null)
        {
            return await QueryAsync(page, pageSize, sortColumn, sortDirection, predicate, null!);
        }

        public async Task<PagedList<TEntity>> QueryAsync(
            int page,
            int pageSize,
            string sortColumn,
            string sortDirection,
            Expression<Func<TEntity, bool>>? predicate = null,
            params Expression<Func<TEntity, object>>[] includes)
        {
            var _query = SetFiltersToQuery(predicate, includes);

            var count = await _query.CountAsync();

            IReadOnlyList<TEntity>? result = null;

            if (string.IsNullOrEmpty(sortColumn))
            {
                result = await _query
                 .OrderByDescending("CreatedDateUtc")
                 .Skip(Skip(page, pageSize))
                 .Take(pageSize)
                 .ToListAsync();

                return new PagedList<TEntity>(count, page, pageSize, sortColumn, sortDirection, result);
            }

            if (string.Equals(sortDirection, "desc", StringComparison.OrdinalIgnoreCase))
            {
                result = await _query
                 .OrderByDescending(sortColumn.ToUpperCaseFirst())
                 .Skip(Skip(page, pageSize))
                 .Take(pageSize)
                 .ToListAsync();

                return new PagedList<TEntity>(count, page, pageSize, sortColumn, sortDirection, result);
            }
            else
            {
                result = await _query
                 .OrderBy(sortColumn.ToUpperCaseFirst())
                 .Skip(Skip(page, pageSize))
                 .Take(pageSize)
                 .ToListAsync();

                return new PagedList<TEntity>(count, page, pageSize, sortColumn, sortDirection, result);
            }
        }

        public async Task<TEntity?> QueryFirstAsync(Expression<Func<TEntity, bool>>? predicate = null)
        {
            return await QueryFirstAsync(predicate, null);
        }

        public async Task<TEntity?> QueryFirstAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            params Expression<Func<TEntity, object>>[]? includes)
        {
            var _query = SetFiltersToQuery(predicate, includes);

            return await _query.FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await _dbContext
                 .Set<TEntity>()
                 .FindAsync(new object[] { entity.Id });
        }
    }
}
