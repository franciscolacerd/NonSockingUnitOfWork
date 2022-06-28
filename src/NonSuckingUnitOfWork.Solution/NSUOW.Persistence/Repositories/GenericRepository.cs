using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NSUOW.Application.DTOs.Common;
using NSUOW.Application.Extensions;
using NSUOW.Application.Models.Pagination;
using NSUOW.Domain.Common;
using NSUOW.Persistence.Contracts;
using NSUOW.Persistence.Repositories.Common;
using System.Linq.Expressions;

namespace NSUOW.Persistence.Repositories
{
    public class GenericRepository<TEntity, TDto, TContext> : BaseRepository<TEntity, TContext>, IGenericRepository<TEntity, TDto, TContext>
        where TEntity : BaseDomainEntity
        where TDto : BaseDto
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

        public TEntity Add(TEntity entity)
        {
            var entityEntry = _dbContext
                .Set<TEntity>()
                .Add(entity);

            return entityEntry.Entity;
        }

        public async Task UpdateAsync(TEntity entity)
        {
            var entityToUpdate = await GetByIdAsync(entity.Id);

            if (entityToUpdate == null)
                return;

            _dbContext.Entry(entityToUpdate).CurrentValues.SetValues(entity);
        }

        public void Update(TEntity entity)
        {
            var entityToUpdate = GetById(entity.Id);

            if (entityToUpdate == null)
                return;

            _dbContext.Entry(entityToUpdate).CurrentValues.SetValues(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entitytoDelete = await GetByIdAsync(id);

            if (entitytoDelete == null)
                return;

            _dbContext.Remove(entitytoDelete);
        }
        public void Delete(int id)
        {
            var entitytoDelete = GetById(id);

            if (entitytoDelete == null)
                return;

            _dbContext.Remove(entitytoDelete);
        }

        public async Task<IReadOnlyList<TEntity>> GetAllAsync()
        {
            return await _dbContext
                .Set<TEntity>()
                .AsNoTracking()
                .ToListAsync();
        }

        public IReadOnlyList<TEntity> GetAll()
        {
            return _dbContext
                .Set<TEntity>()
                .AsNoTracking()
                .ToList();
        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {
            var key = _dbContext.Model.FindEntityType(typeof(TEntity))?.FindPrimaryKey()?.Properties.Single().Name;

            if (key == null) { return null; }

            return await _dbContext
                .Set<TEntity>()
                //.AsNoTracking()
                .Where(x => id.Equals(EF.Property<long>(x, key)))
                .FirstOrDefaultAsync();
        }

        public TEntity? GetById(int id)
        {
            var key = _dbContext.Model.FindEntityType(typeof(TEntity))?.FindPrimaryKey()?.Properties.Single().Name;

            if (key == null) { return null; }

            return _dbContext
                .Set<TEntity>()
                //.AsNoTracking()
                .Where(x => id.Equals(EF.Property<long>(x, key)))
                .FirstOrDefault();
        }

        public async Task<IReadOnlyList<TEntity>> QueryAsync(Expression<Func<TEntity, bool>>? predicate = null)
        {
            return await QueryAsync(predicate, null);
        }

        public IReadOnlyList<TEntity> Query(Expression<Func<TEntity, bool>>? predicate = null)
        {
            return Query(predicate, null);
        }

        public async Task<IReadOnlyList<TEntity>> QueryAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            params Expression<Func<TEntity, object>>[]? includes)
        {
            var _query = SetFiltersToQuery(TrackChanges.AsNoTracking, predicate, includes);

            return await _query.ToListAsync();
        }

        public IReadOnlyList<TEntity> Query(
            Expression<Func<TEntity, bool>>? predicate = null,
            params Expression<Func<TEntity, object>>[]? includes)
        {
            var _query = SetFiltersToQuery(TrackChanges.AsNoTracking, predicate, includes);

            return _query.ToList();
        }

        public async Task<PagedResult<TDto>> QueryAsync(
            int page,
            int pageSize,
            Expression<Func<TEntity, bool>>? predicate = null)
        {
            var _query = SetFiltersToQuery(TrackChanges.AsNoTracking, predicate);

            return await _query.GetPagedAsync<TEntity, TDto>(page, pageSize, _mapper);
        }

        public PagedResult<TDto> Query(
            int page,
            int pageSize,
            Expression<Func<TEntity, bool>>? predicate = null)
        {
            var _query = SetFiltersToQuery(TrackChanges.AsNoTracking, predicate);

            return _query.GetPaged<TEntity, TDto>(page, pageSize, _mapper);
        }

        public async Task<PagedResult<TDto>> QueryAsync(
            int page,
            int pageSize,
            Expression<Func<TEntity, bool>>? predicate = null,
            params Expression<Func<TEntity, object>>[]? includes)
        {
            var _query = SetFiltersToQuery(TrackChanges.AsNoTracking, predicate, includes);

            return await _query.GetPagedAsync<TEntity, TDto>(page, pageSize, _mapper);
        }

        public PagedResult<TDto> Query(
            int page,
            int pageSize,
            Expression<Func<TEntity, bool>>? predicate = null,
            params Expression<Func<TEntity, object>>[]? includes)
        {
            var _query = SetFiltersToQuery(TrackChanges.AsNoTracking, predicate, includes);

            return _query.GetPaged<TEntity, TDto>(page, pageSize, _mapper);
        }

        public async Task<PagedResult<TDto>> QueryAsync(
            int page,
            int pageSize,
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            params Expression<Func<TEntity, object>>[] includes)
        {
            var _query = SetFiltersToQuery(TrackChanges.AsNoTracking, predicate, includes, orderBy);

            return await _query.GetPagedAsync<TEntity, TDto>(page, pageSize, _mapper);
        }

        public PagedResult<TDto> Query(
            int page,
            int pageSize,
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            params Expression<Func<TEntity, object>>[] includes)
        {
            var _query = SetFiltersToQuery(TrackChanges.AsNoTracking, predicate, includes, orderBy);

            return _query.GetPaged<TEntity, TDto>(page, pageSize, _mapper);
        }

        public async Task<TEntity?> QueryFirstAsync(Expression<Func<TEntity, bool>>? predicate = null)
        {
            return await QueryFirstAsync(predicate, null, null);
        }

        public TEntity? QueryFirst(Expression<Func<TEntity, bool>>? predicate = null)
        {
            return QueryFirst(predicate, null, null);
        }

        public async Task<TEntity?> QueryFirstAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            params Expression<Func<TEntity, object>>[]? includes)
        {
            var _query = SetFiltersToQuery(TrackChanges.AsNoTracking, predicate, includes);

            return await _query.FirstOrDefaultAsync();
        }

        public TEntity? QueryFirst(
            Expression<Func<TEntity, bool>>? predicate = null,
            params Expression<Func<TEntity, object>>[]? includes)
        {
            var _query = SetFiltersToQuery(TrackChanges.AsNoTracking, predicate, includes);

            return _query.FirstOrDefault();
        }

        public async Task<TEntity?> QueryFirstAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            params Expression<Func<TEntity, object>>[]? includes)
        {
            var _query = SetFiltersToQuery(TrackChanges.AsNoTracking, predicate, includes, orderBy);

            return await _query.FirstOrDefaultAsync();
        }

        public TEntity? QueryFirst(
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            params Expression<Func<TEntity, object>>[]? includes)
        {
            var _query = SetFiltersToQuery(TrackChanges.AsNoTracking, predicate, includes, orderBy);

            return _query.FirstOrDefault();
        }
    }
}
