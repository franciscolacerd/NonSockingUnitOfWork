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

**Nice functionalities for quering, pagination, sorting, includes and so on:**


        Task<IReadOnlyList<TEntity>> QueryAsync(Expression<Func<TEntity, bool>>? predicate = null);

        Task<IReadOnlyList<TEntity>> QueryAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            params Expression<Func<TEntity, object>>[]? includes);

        Task<PagedResult<TDto>> QueryAsync(
            int page,
            int pageSize,
            Expression<Func<TEntity, bool>>? predicate = null);

        Task<PagedResult<TDto>> QueryAsync(
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

        Task<TEntity?> QueryFirstAsync(Expression<Func<TEntity, bool>>? predicate = null);

        Task<TEntity?> QueryFirstAsync(Expression<Func<TEntity, bool>>? predicate = null,
            params Expression<Func<TEntity, object>>[]? includes);
	    
	Task<TEntity?> QueryFirstAsync(Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            params Expression<Func<TEntity, object>>[]? includes);    
            
**The generic unit of work:**            
            
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Delivery, DeliveryDto, NsuowDbContext> DeliveryRepository { get; }
        IGenericRepository<Package, PackageDto, NsuowDbContext> PackageRepository { get; }

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

`var delivery = await _unitOfWork.DeliveryRepository.QueryFirstAsync(predicate: x => x.BarCode == barcode)`

*Query for entity with child include:*

`var delivery = await _unitOfWork.DeliveryRepository.QueryFirstAsync(predicate: x => x.BarCode == barcode, includes: x => x.Packages)`

*Query for entity list:*

`var pagedDeliveries = await _unitOfWork.DeliveryRepository.QueryAsync(1, 20, predicate: x => x.ReceiverName == "francisco lacerda");`

*Query for entity list with child include:*

`var pagedDeliveries = await _unitOfWork.DeliveryRepository.QueryAsync(1, 20, predicate: x => x.ReceiverName == "francisco lacerda", includes: x => x.Packages);`

*Query for entity list with child include order by CreatedDateUtc:*

`var pagedDeliveries = await _unitOfWork.DeliveryRepository.QueryAsync(1, 20, predicate: x => x.ReceiverName == "francisco lacerda", orderBy: x => x.OrderBy(y => y.CreatedDateUtc), includes: x => x.Packages);`

*Use transactions:*

	await _unitOfWork.BeginTransactionAsync();

	var firstDelivery = await _unitOfWork.DeliveryRepository.AddAsync(delivery);

	var secondDelivery = await _unitOfWork.DeliveryRepository.AddAsync(delivery);

	await _unitOfWork.CommitTransactionAsync();
	
	await _unitOfWork.CompleteAsync();

-----

**NOTE:**

Interfaces (IGenericRepository and IGenericRepositor) are in Infrastructure/Persistence layer because of generic Unit Of Work and generic Repository which implements TEntity of type BaseDomainEntity. If implemented in Core/Application Layer as it should, it would cause cyclic dependency. 


