# Non Sucking Unit Of Work

Tired of the same old c# unit of work? Here is a so generic unit of work that I doubt that it works (joking!).

Also, keep in mind that repository and unit of work with entity framework are antipatterns! Entity Framework DbContext (Core) is already a unit of work!

[No need for repositories and unit of work with Entity Framework Core](https://gunnarpeipman.com/ef-core-repository-unit-of-work/ "No need for repositories and unit of work with Entity Framework Core")

-----
## The good old repository pattern and unit of work with some nice functionalities:


**The good old repository pattern:**

        Task<TDto?> AddAsync(TEntity entity);
		
        Task DeleteAsync(TEntity entity);
		
        Task<IReadOnlyList<TDto>> GetAllAsync();
		
        Task<TDto?> GetByIdAsync(int id);
		
        Task UpdateAsync(TEntity entity);

**Nice functionalities for quering, pagination,sorting, include and so on:**


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
            
**The generic unit of work:**            
            

    public class UnitOfWork : IUnitOfWork
    {
        private readonly IMapper _mapper;

        private readonly NsuowDbContext _context;

        private IDbContextTransaction _transaction;

        private readonly IHttpContextAccessor _httpContextAccessor;

        private IGenericRepository<Delivery, DeliveryDto, NsuowDbContext> _deliveryRepository;

        private IGenericRepository<Package, PackageDto, NsuowDbContext> _packageRepository;

        public UnitOfWork(NsuowDbContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public IGenericRepository<Delivery, DeliveryDto, NsuowDbContext> DeliveryRepository => 
                          _deliveryRepository ??= new GenericRepository<Delivery, DeliveryDto, NsuowDbContext>(_context, _mapper);

        public IGenericRepository<Package, PackageDto, NsuowDbContext> PackageRepository => 
                          _packageRepository ??= new GenericRepository<Package, PackageDto, NsuowDbContext>(_context, _mapper);



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

`await _unitOfWork.DeliveryRepository.QueryFirstAsync(x => x.BarCode == barcode)`




More documentation in the near future.

>More unit tests in the near future (sorry red, green, refactor...).
