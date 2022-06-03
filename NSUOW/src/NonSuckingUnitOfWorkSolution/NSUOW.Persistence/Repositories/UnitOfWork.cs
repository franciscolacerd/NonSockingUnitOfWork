using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage;
using NSUOW.Application.DTOs;
using NSUOW.Persistence.Contracts;
using NSWOF.Domain;
using System.Security.Claims;

namespace NSUOW.Persistence.Repositories
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : BaseDbContext
    {
        private readonly IMapper _mapper;

        private readonly TContext _context;

        private IDbContextTransaction _transaction;

        private readonly IHttpContextAccessor _httpContextAccessor;

        private GenericRepository<Service, ServiceDto, TContext> _serviceRepository;

        private GenericRepository<Volume, VolumeDto, TContext> _volumeRepository;

        public UnitOfWork(TContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public GenericRepository<Service, ServiceDto, TContext> ServiceRepository => _serviceRepository ??= new GenericRepository<Service, ServiceDto, TContext>(_context, _mapper);

        public GenericRepository<Volume, VolumeDto, TContext> VolumeRepository => _volumeRepository ??= new GenericRepository<Volume, VolumeDto, TContext>(_context, _mapper);

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            await _transaction.CommitAsync(cancellationToken);
        }

        public async Task<int> CompleteAsync(CancellationToken cancellationToken = default)
        {
            var username = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;

            return await _context.SaveChangesAsync(cancellationToken, username!);
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
