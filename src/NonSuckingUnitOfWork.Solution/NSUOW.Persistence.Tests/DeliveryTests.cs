using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NSUOW.Application.Extensions;
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

        private IUnitOfWork _unitOfWork;

        [SetUp]
        public void Setup()
        {
            _serviceProvider = Bootstrapper.Bind();

            _unitOfWork = _serviceProvider.GetRequiredService<IUnitOfWork>();
        }

        [Test]
        public async Task Delivery_CreateDeliveryWithPackages_ReturnDelivery()
        {
            await GetAndAssertDeliveryAsync();
        }

        [Test]
        public async Task Delivery_DeliveryGetByBarCode_ReturnDeliveryWithPackages()
        {
            string barcode = await GetAndAssertDeliveryAsync();

            var newDelivery = await _unitOfWork.DeliveryRepository.QueryFirstAsync(x => x.BarCode == barcode);

            newDelivery?.Should().NotBeNull();

            newDelivery?.BarCode.Should().NotBeNull();

            newDelivery?.BarCode.Should().BeEquivalentTo(barcode);
        }

        private async Task<string> GetAndAssertDeliveryAsync()
        {
            var barcode = 15.ToRandomStringOfInts();

            var delivery = GetDummyDelivery(barcode);

            var result = await _unitOfWork.DeliveryRepository.AddAsync(delivery);

            await _unitOfWork.SaveChangesAsync();

            result?.Should().NotBeNull();

            result?.BarCode.Should().NotBeNull();

            result?.BarCode.Should().BeEquivalentTo(barcode);

            return barcode;
        }
    }
}