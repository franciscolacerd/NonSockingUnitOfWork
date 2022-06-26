using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NSUOW.Persistence.Contracts;
using NSUOW.Persistence.Tests.Common;
using NUnit.Framework;
using ServicesApi.Services.Tests.Strapper;
using System.Linq;
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

            delivery.Packages.Should().HaveCountGreaterThan(0);
        }

        [Test]
        public async Task Delivery_DeliveryGetPagedListByName_ReturnDeliveryWithPackages()
        {
            var pagedDeliveries = await _unitOfWork.DeliveryRepository.QueryAsync(1, 20, predicate: x => x.ReceiverName == "francisco lacerda");

            pagedDeliveries.Should().NotBeNull();

            pagedDeliveries.Results.Should().HaveCountGreaterThan(0);

            pagedDeliveries.PageCount.Should().BeGreaterThan(0);

            pagedDeliveries.RowCount.Should().BeGreaterThan(0);
        }

        [Test]
        public async Task Delivery_DeliveryGetPagedListByNameIncludeChild_ReturnDeliveryWithPackages()
        {
            var pagedDeliveries = await _unitOfWork.DeliveryRepository.QueryAsync(
                1,
                20,
                predicate: x => x.ReceiverName == "francisco lacerda",
                includes: x => x.Packages);

            pagedDeliveries.Should().NotBeNull();

            pagedDeliveries.Results.Should().HaveCountGreaterThan(0);

            pagedDeliveries.PageCount.Should().BeGreaterThan(0);

            pagedDeliveries.RowCount.Should().BeGreaterThan(0);

            pagedDeliveries.Results.First().Packages.Should().NotBeNull();

            pagedDeliveries.Results.First().Packages.Should().HaveCountGreaterThan(0);
        }

        [Test]
        public async Task Delivery_DeliveryGetPagedListByNameIncludeChildOrderByCreateDateUtc_ReturnDeliveryWithPackages()
        {
            var pagedDeliveries = await _unitOfWork.DeliveryRepository.QueryAsync(
                1,
                20,
                predicate: x => x.ReceiverName == "francisco lacerda",
                orderBy: x => x.OrderBy(y => y.CreatedDateUtc),
                includes: x => x.Packages);

            pagedDeliveries.Should().NotBeNull();

            pagedDeliveries.Results.Should().HaveCountGreaterThan(0);

            pagedDeliveries.PageCount.Should().BeGreaterThan(0);

            pagedDeliveries.RowCount.Should().BeGreaterThan(0);

            pagedDeliveries.Results.First().Packages.Should().NotBeNull();

            pagedDeliveries.Results.First().Packages.Should().HaveCountGreaterThan(0);
        }
    }
}