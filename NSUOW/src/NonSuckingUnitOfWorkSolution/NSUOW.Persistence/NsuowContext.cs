﻿using Microsoft.EntityFrameworkCore;
using NSUOW.Persistence.Repositories;
using NSWOF.Domain;

namespace NSUOW.Persistence
{
    public class NsuowContext : BaseDbContext
    {
        public NsuowContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(NsuowContext).Assembly);
        }

        public DbSet<Service> Services { get; set; } = null!;

        public DbSet<Volume> Volumes { get; set; } = null!;
    }
}