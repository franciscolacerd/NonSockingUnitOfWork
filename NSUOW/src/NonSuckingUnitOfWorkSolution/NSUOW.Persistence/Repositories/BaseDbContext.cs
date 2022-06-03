using Microsoft.EntityFrameworkCore;
using NSUOW.Domain.Common;

namespace NSUOW.Persistence.Repositories
{
    public class BaseDbContext : DbContext
    {
        public BaseDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual async Task<int> SaveChangesAsync(string username = "SYSTEM")
        {
            foreach (var entry in base.ChangeTracker.Entries<BaseDomainEntity>()
                .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
            {
                entry.Entity.UpdatedDateUtc = DateTime.UtcNow;
                entry.Entity.UpdatedBy = username;

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedDateUtc = DateTime.UtcNow;
                    entry.Entity.CreatedBy = username;
                }
            }

            return await base.SaveChangesAsync();
        }

        public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default, string username = "SYSTEM")
        {
            foreach (var entry in base.ChangeTracker.Entries<BaseDomainEntity>()
                .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
            {
                entry.Entity.UpdatedDateUtc = DateTime.UtcNow;
                entry.Entity.UpdatedBy = username;

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedDateUtc = DateTime.UtcNow;
                    entry.Entity.CreatedBy = username;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
