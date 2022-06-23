using NSUOW.Domain;

namespace NSUOW.Persistence.Contracts
{
    public interface IUnitOfWork : IDisposable
    {

        IGenericRepository<Delivery, NsuowDbContext> DeliveryRepository { get; }
        IGenericRepository<Package, NsuowDbContext> PackageRepository { get; }

        Task BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task CommitTransactionAsync(CancellationToken cancellationToken = default);
        Task<int> CompleteAsync(CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
