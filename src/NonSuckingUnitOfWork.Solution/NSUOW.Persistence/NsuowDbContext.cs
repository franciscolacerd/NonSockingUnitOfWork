﻿using Microsoft.EntityFrameworkCore;
using NSUOW.Domain;
using NSUOW.Persistence.Repositories.Common;

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

        public DbSet<Delivery> Deliveries { get; set; } = null!;

        public DbSet<Package> Packages { get; set; } = null!;
    }
}
