using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage;
using NSUOW.Application.DTOs;
using NSUOW.Domain;
using NSUOW.Persistence.Contracts;
using System.Security.Claims;

namespace NSUOW.Persistence.Repositories
{
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

        public IGenericRepository<Delivery, DeliveryDto, NsuowDbContext> DeliveryRepository => _deliveryRepository ??= new GenericRepository<Delivery, DeliveryDto, NsuowDbContext>(_context, _mapper);

        public IGenericRepository<Package, PackageDto, NsuowDbContext> PackageRepository => _packageRepository ??= new GenericRepository<Package, PackageDto, NsuowDbContext>(_context, _mapper);

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            await _transaction.CommitAsync(cancellationToken);
        }

        public void BeginTransaction()
        {
            _transaction =  _context.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _transaction.Commit();
        }

        public async Task<int> CompleteAsync(CancellationToken cancellationToken = default)
        {
            var username = _httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;

            return await _context.SaveChangesAsync(username, cancellationToken);
        }

        public int Complete()
        {
            var username = _httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;

            return _context.SaveChanges(username);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await CompleteAsync(cancellationToken);
        }

        public int SaveChanges()
        {
            return Complete();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _transaction?.Dispose();
                _context.Dispose();
            }
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}