using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSUOW.Persistence;
using NUnit.Framework;
using System;


namespace ServicesApi.Services.Tests.Strapper
{
    public static class Bootstrapper
    {
        public static IConfigurationRoot GetIConfigurationRoot(string outputPath)
        {
            return new ConfigurationBuilder()
                .SetBasePath(outputPath)
                .AddJsonFile("appsettings.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
        }

        public static ServiceProvider Bind()
        {
            var services = new ServiceCollection();

            services.AddOptions();

            services.AddLogging();

            var configurationRoot = GetIConfigurationRoot(TestContext.CurrentContext.TestDirectory);

            services.AddDbContext<NsuowContext>(opts => opts.UseSqlServer(configurationRoot.GetConnectionString("DbContext"),
                options =>
                {
                    options.EnableRetryOnFailure(maxRetryCount: 3, maxRetryDelay: TimeSpan.FromSeconds(5), null);
                    options.CommandTimeout(99999);
                }));

            services.ConfigurePersistenceServices();

            services.AddSingleton<IConfiguration>(configurationRoot);

            return services.BuildServiceProvider();
        }
    }
}