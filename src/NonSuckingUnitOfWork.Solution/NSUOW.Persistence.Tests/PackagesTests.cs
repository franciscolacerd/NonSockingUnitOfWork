using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NSUOW.Persistence.Contracts;
using NSUOW.Persistence.Tests.Common;
using NUnit.Framework;
using ServicesApi.Services.Tests.Strapper;
using System.Threading.Tasks;

namespace NSUOW.Persistence.Tests
{
    public class PackagesTests : BaseTest
    {
        private ServiceProvider _serviceProvider;

        [SetUp]
        public void Setup()
        {
            _serviceProvider = Bootstrapper.Bind();

            _unitOfWork = _serviceProvider.GetRequiredService<IUnitOfWork>();
        }

        [Test]
        public async Task Package_PackageGetById_ReturnPackageWithDeliveryAsync()
        {
            var dummyDelivery = await GetAndAssertDeliveryAsync();

            var package = await _unitOfWork.PackageRepository.QueryFirstAsync(x => x.DeliveryId == dummyDelivery.Id);

            package.Should().NotBeNull();
        }
    }
}