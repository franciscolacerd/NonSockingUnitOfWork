using Microsoft.EntityFrameworkCore;
using NSUOW.Domain.Common;
using System.Linq.Expressions;

namespace NSUOW.Persistence.Repositories.Common
{
    public class BaseRepository<TEntity, TContext>
           where TEntity : BaseDomainEntity
           where TContext : DbContext
    {
        private readonly TContext _dbContext;

        public BaseRepository(TContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<TEntity> SetFiltersToQuery(
            TrackChanges trackChanges,
            Expression<Func<TEntity, bool>>? predicate)
        {
            return SetFiltersToQuery(trackChanges, predicate, null);
        }

        public IQueryable<TEntity> SetFiltersToQuery(
             TrackChanges trackChanges,
             Expression<Func<TEntity, bool>>? predicate,
             Expression<Func<TEntity, object>>[]? includes)
        {
            return SetFiltersToQuery(trackChanges, predicate, includes, null);
        }

        public IQueryable<TEntity> SetFiltersToQuery(
             TrackChanges trackChanges,
             Expression<Func<TEntity, bool>>? predicate,
             Expression<Func<TEntity, object>>[]? includes,
             Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null)
        {
            var dbSet = _dbContext.Set<TEntity>();

            IQueryable<TEntity> _query = dbSet;

            if (trackChanges == TrackChanges.AsNoTracking)
                _query = _query.AsNoTracking();

            if (predicate != null)
                _query = _query.Where(predicate);

            if (includes != null)
                _query = includes.Aggregate(_query, (current, include) => current.Include(include));

            if (orderBy != null)
                return orderBy(_query);

            return _query;
        }
    }
}
