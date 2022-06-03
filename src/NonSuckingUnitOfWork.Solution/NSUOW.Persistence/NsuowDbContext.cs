using Microsoft.EntityFrameworkCore;
using NSUOW.Domain;
using NSUOW.Persistence.Repositories;

namespace NSUOW.Persistence
{
    public class NsuowDbContext : BaseDbContext
    {
        public NsuowDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(NsuowDbContext).Assembly);
        }

        public DbSet<Service> Services { get; set; } = null!;

        public DbSet<Volume> Volumes { get; set; } = null!;
    }
}
