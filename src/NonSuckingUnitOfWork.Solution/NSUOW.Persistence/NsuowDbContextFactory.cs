using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace NSUOW.Persistence
{
    public class NsuowDbContextFactory : IDesignTimeDbContextFactory<NsuowDbContext>
    {
        public NsuowDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<NsuowDbContext>();
            var connectionString = configuration.GetConnectionString("DbContext");

            builder.UseSqlServer(connectionString);

            return new NsuowDbContext(builder.Options);
        }
    }
}
