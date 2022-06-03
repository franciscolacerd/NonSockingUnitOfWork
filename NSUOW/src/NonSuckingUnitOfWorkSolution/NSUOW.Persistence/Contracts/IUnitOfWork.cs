using NSUOW.Application.DTOs;
using NSUOW.Domain;
using NSUOW.Persistence.Repositories;

namespace NSUOW.Persistence.Contracts
{
    public interface IUnitOfWork<TContext> : IDisposable where TContext : BaseDbContext
    {

        GenericRepository<Service, ServiceDto, TContext> ServiceRepository { get; }
        GenericRepository<Volume, VolumeDto, TContext> VolumeRepository { get; }

        Task BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task CommitTransactionAsync(CancellationToken cancellationToken = default);
        Task<int> CompleteAsync(CancellationToken cancellationToken = default);
    }
}
