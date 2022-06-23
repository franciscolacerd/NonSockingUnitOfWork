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
    public class DeliveriesTests : BaseTest
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
        public async Task Delivery_CreateDeliveryWithPackages_ReturnDelivery()
        {
            await GetAndAssertDeliveryAsync();
        }

        [Test]
        public async Task Delivery_DeliveryGetByBarCode_ReturnDelivery()
        {
            string barcode = await GetAndAssertDeliveryAsync();

            var delivery = await _unitOfWork.DeliveryRepository.QueryFirstAsync(x => x.BarCode == barcode);

            delivery.Should().NotBeNull();

            delivery.BarCode.Should().NotBeNull();

            delivery.BarCode.Should().BeEquivalentTo(barcode);
        }

        [Test]
        public async Task Delivery_DeliveryGetByBarCodeIncludePackages_ReturnDeliveryWithPackages()
        {
            string barcode = await GetAndAssertDeliveryAsync();

            var delivery = await _unitOfWork.DeliveryRepository.QueryFirstAsync(x => x.BarCode == barcode, include => include.Packages);

            delivery.Should().NotBeNull();

            delivery.BarCode.Should().NotBeNull();

            delivery.BarCode.Should().BeEquivalentTo(barcode);

            delivery.Packages.Should().NotBeNull();
        }
    }
}