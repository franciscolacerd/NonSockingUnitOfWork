using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NSUOW.Persistence.Contracts;
using NSUOW.Persistence.Tests.Common;
using NUnit.Framework;
using ServicesApi.Services.Tests.Strapper;
using System.Threading.Tasks;

namespace NSUOW.Persistence.Tests
{
    public class DeliveryTests : BaseTest
    {

        private ServiceProvider _serviceProvider;

        [SetUp]
        public void Setup()
        {
            _serviceProvider = Bootstrapper.Bind();

            _unitOfWork = _serviceProvider.GetRequiredService<IUnitOfWork>();

            _mapper = _serviceProvider.GetRequiredService<IMapper>();
        }

        [Test]
        public async Task Delivery_DeliveryGetByBarCode_ReturnDeliveryWithPackages()
        {
            var delivery = await GetAndAssertDeliveryAsync(GetBarcode());

            var package = await _unitOfWork.PackageRepository.QueryFirstAsync(x => x.DeliveryId == delivery.Id);

            package.Should().NotBeNull();
        }
    }
}