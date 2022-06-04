using NSUOW.Application.DTOs;
using NSUOW.Domain;

namespace NSUOW.Persistence.Contracts
{
    public interface IUnitOfWork : IDisposable
    {

        IGenericRepository<Service, ServiceDto, NsuowDbContext> ServiceRepository { get; }
        IGenericRepository<Volume, VolumeDto, NsuowDbContext> VolumeRepository { get; }

        Task BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task CommitTransactionAsync(CancellationToken cancellationToken = default);
        Task<int> CompleteAsync(CancellationToken cancellationToken = default);
    }
}
