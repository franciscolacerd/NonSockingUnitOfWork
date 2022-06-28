using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NSUOW.Persistence.Contracts;
using NSUOW.Persistence.Tests.Common;
using NUnit.Framework;
using ServicesApi.Services.Tests.Strapper;
using System;
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
        public async Task Delivery_CreateDeliveryWithPackages_ReturnDeliveryAsync()
        {
            await GetAndAssertDeliveryAsync();
        }

        [Test]
        public async Task Delivery_UpdateDeliveryChangeDeliveryDate_ReturnDeliveryAsync()
        {
            string barcode = await GetAndAssertDeliveryAsync();

            var deliveryDate = DateTime.UtcNow.AddDays(2);

            var delivery = await _unitOfWork.DeliveryRepository.QueryFirstAsync(predicate: x => x.BarCode == barcode);

            delivery.Should().NotBeNull();

            delivery.DeliveryDate = deliveryDate;

            await _unitOfWork.DeliveryRepository.UpdateAsync(delivery);

            await _unitOfWork.SaveChangesAsync();

            delivery = await _unitOfWork.DeliveryRepository.QueryFirstAsync(predicate: x => x.BarCode == barcode);

            delivery.Should().NotBeNull();

            delivery.DeliveryDate.Should().Be(deliveryDate);
        }

        [Test]
        public async Task Delivery_DeleteDeliveryChangeDeliveryDate_ReturnDeliveryAsync()
        {
            string barcode = await GetAndAssertDeliveryAsync();

            var delivery = await _unitOfWork.DeliveryRepository.QueryFirstAsync(predicate: x => x.BarCode == barcode);

            delivery.Should().NotBeNull();

            await _unitOfWork.DeliveryRepository.DeleteAsync(delivery.Id);

            await _unitOfWork.SaveChangesAsync();

            delivery = await _unitOfWork.DeliveryRepository.QueryFirstAsync(predicate: x => x.BarCode == barcode);

            delivery.Should().BeNull();
        }


        [Test]
        public async Task Delivery_DeliveryGetById_ReturnDeliveryAsync()
        {
            var service = await GetAndAssertDeliveryAsync(GetBarcode());

            var delivery = await _unitOfWork.DeliveryRepository.GetByIdAsync(service.Id);

            delivery.Should().NotBeNull();
        }

        [Test]
        public async Task Delivery_DeliveryGetByBarCode_ReturnDeliveryAsync()
        {
            string barcode = await GetAndAssertDeliveryAsync();

            var delivery = await _unitOfWork.DeliveryRepository.QueryFirstAsync(predicate: x => x.BarCode == barcode);

            delivery.Should().NotBeNull();

            delivery.BarCode.Should().NotBeNull();

            delivery.BarCode.Should().BeEquivalentTo(barcode);
        }

        [Test]
        public async Task Delivery_DeliveryGetByBarCodeIncludePackages_ReturnDeliveryWithPackagesAsync()
        {
            string barcode = await GetAndAssertDeliveryAsync();

            var delivery = await _unitOfWork.DeliveryRepository.QueryFirstAsync(predicate: x => x.BarCode == barcode, includes: x => x.Packages);

            delivery.Should().NotBeNull();

            delivery.BarCode.Should().NotBeNull();

            delivery.BarCode.Should().BeEquivalentTo(barcode);

            delivery.Packages.Should().NotBeNull();

            delivery.Packages.Should().HaveCountGreaterThan(0);
        }

        [Test]
        public async Task Delivery_DeliveryGetByBarCodeIncludePackagesOrderBy_ReturnDeliveryWithPackagesAsync()
        {
            string barcode = await GetAndAssertDeliveryAsync();

            var delivery = await _unitOfWork.DeliveryRepository.QueryFirstAsync(
                predicate: x => x.BarCode == barcode,
                orderBy: x => x.OrderByDescending(y => y.CreatedDateUtc),
                includes: x => x.Packages);

            delivery.Should().NotBeNull();

            delivery.BarCode.Should().NotBeNull();

            delivery.BarCode.Should().BeEquivalentTo(barcode);

            delivery.Packages.Should().NotBeNull();

            delivery.Packages.Should().HaveCountGreaterThan(0);
        }

        [Test]
        public async Task Delivery_DeliveryGetPagedListByName_ReturnDeliveryWithPackagesAsync()
        {
            var pagedDeliveries = await _unitOfWork.DeliveryRepository.QueryAsync(
                1,
                20,
                predicate: x => x.ReceiverName == "francisco lacerda");

            pagedDeliveries.Should().NotBeNull();

            pagedDeliveries.Results.Should().HaveCountGreaterThan(0);

            pagedDeliveries.PageCount.Should().BeGreaterThan(0);

            pagedDeliveries.RowCount.Should().BeGreaterThan(0);
        }

        [Test]
        public async Task Delivery_DeliveryGetPagedListByNameIncludeChild_ReturnDeliveryWithPackagesAsync()
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
        public async Task Delivery_DeliveryGetPagedListByNameIncludeChildOrderByCreateDateUtc_ReturnDeliveryWithPackagesAsync()
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

        [Test]
        public void Delivery_CreateDeliveryWithPackages_ReturnDelivery()
        {
            GetAndAssertDelivery();
        }

        [Test]
        public void Delivery_UpdateDeliveryChangeDeliveryDate_ReturnDelivery()
        {
            string barcode = GetAndAssertDelivery();

            var deliveryDate = DateTime.UtcNow.AddDays(2);

            var delivery = _unitOfWork.DeliveryRepository.QueryFirst(predicate: x => x.BarCode == barcode);

            delivery.Should().NotBeNull();

            delivery.DeliveryDate = deliveryDate;

            _unitOfWork.DeliveryRepository.Update(delivery);

            _unitOfWork.SaveChanges();

            delivery =  _unitOfWork.DeliveryRepository.QueryFirst(predicate: x => x.BarCode == barcode);

            delivery.Should().NotBeNull();

            delivery.DeliveryDate.Should().Be(deliveryDate);
        }

        [Test]
        public void Delivery_DeleteDeliveryChangeDeliveryDate_ReturnDelivery()
        {
            string barcode = GetAndAssertDelivery();

            var delivery = _unitOfWork.DeliveryRepository.QueryFirst(predicate: x => x.BarCode == barcode);

            delivery.Should().NotBeNull();

            _unitOfWork.DeliveryRepository.Delete(delivery.Id);

            _unitOfWork.SaveChanges();

            delivery = _unitOfWork.DeliveryRepository.QueryFirst(predicate: x => x.BarCode == barcode);

            delivery.Should().BeNull();
        }

        [Test]
        public void Delivery_DeliveryGetById_ReturnDelivery()
        {
            var service = GetAndAssertDelivery(GetBarcode());

            var delivery = _unitOfWork.DeliveryRepository.GetById(service.Id);

            delivery.Should().NotBeNull();
        }

        [Test]
        public void Delivery_DeliveryGetByBarCode_ReturnDelivery()
        {
            string barcode = GetAndAssertDelivery();

            var delivery = _unitOfWork.DeliveryRepository.QueryFirst(predicate: x => x.BarCode == barcode);

            delivery.Should().NotBeNull();

            delivery.BarCode.Should().NotBeNull();

            delivery.BarCode.Should().BeEquivalentTo(barcode);
        }

        [Test]
        public void Delivery_DeliveryGetByBarCodeIncludePackages_ReturnDeliveryWithPackages()
        {
            string barcode = GetAndAssertDelivery();

            var delivery = _unitOfWork.DeliveryRepository.QueryFirst(predicate: x => x.BarCode == barcode, includes: x => x.Packages);

            delivery.Should().NotBeNull();

            delivery.BarCode.Should().NotBeNull();

            delivery.BarCode.Should().BeEquivalentTo(barcode);

            delivery.Packages.Should().NotBeNull();

            delivery.Packages.Should().HaveCountGreaterThan(0);
        }

        [Test]
        public void Delivery_DeliveryGetByBarCodeIncludePackagesOrderBy_ReturnDeliveryWithPackages()
        {
            string barcode = GetAndAssertDelivery();

            var delivery = _unitOfWork.DeliveryRepository.QueryFirst(
                predicate: x => x.BarCode == barcode,
                orderBy: x => x.OrderByDescending(y => y.CreatedDateUtc),
                includes: x => x.Packages);

            delivery.Should().NotBeNull();

            delivery.BarCode.Should().NotBeNull();

            delivery.BarCode.Should().BeEquivalentTo(barcode);

            delivery.Packages.Should().NotBeNull();

            delivery.Packages.Should().HaveCountGreaterThan(0);
        }

        [Test]
        public void Delivery_DeliveryGetPagedListByName_ReturnDeliveryWithPackages()
        {
            var pagedDeliveries = _unitOfWork.DeliveryRepository.Query(
                1,
                20,
                predicate: x => x.ReceiverName == "francisco lacerda");

            pagedDeliveries.Should().NotBeNull();

            pagedDeliveries.Results.Should().HaveCountGreaterThan(0);

            pagedDeliveries.PageCount.Should().BeGreaterThan(0);

            pagedDeliveries.RowCount.Should().BeGreaterThan(0);
        }

        [Test]
        public void Delivery_DeliveryGetPagedListByNameIncludeChild_ReturnDeliveryWithPackages()
        {
            var pagedDeliveries = _unitOfWork.DeliveryRepository.Query(
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
        public void Delivery_DeliveryGetPagedListByNameIncludeChildOrderByCreateDateUtc_ReturnDeliveryWithPackages()
        {
            var pagedDeliveries = _unitOfWork.DeliveryRepository.Query(
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

        [Test]

        public async Task Delivery_CreateServicesWithTransaction_TransactionsSuccess()
        {
            var delivery = GetDummyDelivery(GetBarcode());

            await _unitOfWork.BeginTransactionAsync();

            var firstDelivery = await _unitOfWork.DeliveryRepository.AddAsync(delivery);

            firstDelivery.Should().NotBeNull();

            var secondDelivery = await _unitOfWork.DeliveryRepository.AddAsync(delivery);

            secondDelivery.Should().NotBeNull();

            await _unitOfWork.CommitTransactionAsync();

            await _unitOfWork.CompleteAsync();

            var firstDeliveryCreated = await _unitOfWork.DeliveryRepository.GetByIdAsync(firstDelivery.Id);

            firstDeliveryCreated.Should().NotBeNull();

            var secondDeliveryCreated = await _unitOfWork.DeliveryRepository.GetByIdAsync(secondDelivery.Id);

            secondDeliveryCreated.Should().NotBeNull();
        }
    }
}