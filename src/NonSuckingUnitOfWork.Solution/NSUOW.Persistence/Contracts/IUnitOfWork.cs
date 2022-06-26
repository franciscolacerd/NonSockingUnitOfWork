using NSUOW.Application.DTOs;
using NSUOW.Domain;

namespace NSUOW.Persistence.Contracts
{
    public interface IUnitOfWork : IDisposable
    {

        IGenericRepository<Delivery, DeliveryDto, NsuowDbContext> DeliveryRepository { get; }
        IGenericRepository<Package, PackageDto, NsuowDbContext> PackageRepository { get; }

        Task BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task CommitTransactionAsync(CancellationToken cancellationToken = default);
        Task<int> CompleteAsync(CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
