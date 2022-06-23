# Non Sucking Unit Of Work

Tired of the same old c# unit of work? Here is a so generic unit of work that I doubt that it works (joking!).

Also, keep in mind that repository and unit of work with entity framework are antipatterns! Entity Framework DbContext (Core) is already a unit of work!

[No need for repositories and unit of work with Entity Framework Core](https://gunnarpeipman.com/ef-core-repository-unit-of-work/ "No need for repositories and unit of work with Entity Framework Core")

-----
## The good old repository pattern and unit of work with some nice functionalities:


**The good old repository pattern:**

        Task<TEntity> AddAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);
	
        Task<IReadOnlyList<TEntity>> GetAllAsync();

        Task<TEntity?> GetByIdAsync(int id);
		
        Task UpdateAsync(TEntity entity);

**Nice functionalities for quering, pagination,sorting, include and so on:**


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
            
**The generic unit of work:**            
            
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Delivery, NsuowDbContext> DeliveryRepository { get; }
        IGenericRepository<Package, NsuowDbContext> PackageRepository { get; }

        Task BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task CommitTransactionAsync(CancellationToken cancellationToken = default);
        Task<int> CompleteAsync(CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

-----

**How to use?**

Copy the code and experiment.

**Create database**

Database is pointing to LocalDB.

Runs the scripts for database generation with migrations:

`add-migration dbdeploy -context NSUOW.Persistence.NsuowDbContext -verbose`

And then:

`update-database -context NSUOW.Persistence.NsuowDbContext -verbose`

-----

**Documentation**

*Query for entity:*

`var delivery = await _unitOfWork.DeliveryRepository.QueryFirstAsync(x => x.BarCode == barcode)`

*Query for entity with child include:*

`var delivery = await _unitOfWork.DeliveryRepository.QueryFirstAsync(x => x.BarCode == barcode, include => include.Packages)`


More documentation in the near future.

>More unit tests in the near future (sorry red, green, refactor...).
