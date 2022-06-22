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
    public class ServiceTests : BaseTest
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
        public async Task Service_CreateServiceWithVolumes_ReturnService()
        {
            string serviceBarCode = await GetAndAssertServiceAsync();
        }

        [Test]
        public async Task Service_ServiceGetByServiceBarCode_ReturnServiceWithNoVolumes()
        {
            string serviceBarCode = await GetAndAssertServiceAsync();

            var newService = await _unitOfWork.ServiceRepository.QueryFirstAsync(x => x.ServiceBarCode == serviceBarCode);

            newService?.Should().NotBeNull();

            newService?.ServiceBarCode.Should().NotBeNull();

            newService?.ServiceBarCode.Should().BeEquivalentTo(serviceBarCode);
        }

        private async Task<string> GetAndAssertServiceAsync()
        {
            var serviceBarCode = 15.ToRandomStringOfInts();

            var service = GetDummyService(serviceBarCode);

            var result = await _unitOfWork.ServiceRepository.AddAsync(service);

            await _unitOfWork.SaveChangesAsync();

            result?.Should().NotBeNull();

            result?.ServiceBarCode.Should().NotBeNull();

            result?.ServiceBarCode.Should().BeEquivalentTo(serviceBarCode);

            return serviceBarCode;
        }
    }
}