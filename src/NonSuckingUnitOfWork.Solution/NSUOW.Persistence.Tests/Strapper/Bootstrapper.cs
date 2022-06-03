using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSUOW.Persistence;
using NUnit.Framework;


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

            services.ConfigurePersistenceServices(configurationRoot);

            services.AddSingleton<IConfiguration>(configurationRoot);

            return services.BuildServiceProvider();
        }
    }
}