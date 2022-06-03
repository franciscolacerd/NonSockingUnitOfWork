using NSUOW.Application.DTOs;
using NSUOW.Domain;
using NSUOW.Persistence.Repositories;

namespace NSUOW.Persistence.Contracts
{
    public interface IUnitOfWork : IDisposable
    {

        GenericRepository<Service, ServiceDto, NsuowDbContext> ServiceRepository { get; }
        GenericRepository<Volume, VolumeDto, NsuowDbContext> VolumeRepository { get; }

        Task BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task CommitTransactionAsync(CancellationToken cancellationToken = default);
        Task<int> CompleteAsync(CancellationToken cancellationToken = default);
    }
}
