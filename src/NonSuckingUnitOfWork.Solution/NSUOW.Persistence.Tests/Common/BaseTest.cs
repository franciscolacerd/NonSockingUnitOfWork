using AutoMapper;
using FluentAssertions;
using NSUOW.Application.DTOs;
using NSUOW.Application.Extensions;
using NSUOW.Domain;
using NSUOW.Persistence.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NSUOW.Persistence.Tests.Common
{
    public class BaseTest
    {
        protected IUnitOfWork _unitOfWork;

        protected IMapper _mapper;

        protected async Task<string> GetAndAssertDeliveryAsync()
        {
            var barcode = GetBarcode();

            await GetAndAssertDeliveryAsync(barcode);

            return barcode;
        }

        protected string GetAndAssertDelivery()
        {
            var barcode = GetBarcode();

            GetAndAssertDelivery(barcode);

            return barcode;
        }

        protected static string GetBarcode()
        {
            return 15.ToRandomStringOfInts();
        }

        protected async Task<DeliveryDto> GetAndAssertDeliveryAsync(string barcode)
        {
            var delivery = GetDummyDelivery(barcode);

            var result = await _unitOfWork.DeliveryRepository.AddAsync(delivery);

            await _unitOfWork.SaveChangesAsync();

            result.Should().NotBeNull();

            result.BarCode.Should().NotBeNull();

            result.BarCode.Should().BeEquivalentTo(barcode);

            return _mapper.Map<DeliveryDto>(result);
        }

        protected DeliveryDto GetAndAssertDelivery(string barcode)
        {
            var delivery = GetDummyDelivery(barcode);

            var result = _unitOfWork.DeliveryRepository.Add(delivery);

            _unitOfWork.SaveChanges();

            result.Should().NotBeNull();

            result.BarCode.Should().NotBeNull();

            result.BarCode.Should().BeEquivalentTo(barcode);

            return _mapper.Map<DeliveryDto>(result);
        }

        protected static Delivery GetDummyDelivery(string barcode)
        {
            var totalWeight = 2.ToRandomDecimal();

            return new Delivery
            {
                Amount = null,
                ClientReference = $"test service",
                DeliveryDate = DateTime.UtcNow.AddDays(1),
                Eta = null,
                Instructions = "Handle with care.",
                NumberOfVolumes = 2,
                PickingDate = null,
                PinNumber = 4.ToRandomStringOfInts(),
                PreferentialPeriod = null,
                ReceiverAddress = "Rua Alto do Monte, nº 1",
                ReceiverAddressCountryCode = "PT",
                ReceiverAddressPlace = "Lisboa",
                ReceiverAddressZipCode = "1000-072",
                ReceiverAddressZipCodePlace = "Lisboa",
                ReceiverClientCode = null,
                ReceiverContactEmail = "flacerda@test.pt",
                ReceiverContactName = "francisco lacerda",
                ReceiverContactPhoneNumber = "915579045",
                ReceiverFixedInstructions = null,
                ReceiverName = "francisco lacerda",
                SenderAddress = "Rua Pedra da Ponte, nº 5",
                SenderAddressCountryCode = "PT",
                SenderAddressPlace = "Porto",
                SenderAddressZipCode = "4100-072",
                SenderAddressZipCodePlace = "Porto",
                SenderClientCode = null,
                SenderContactEmail = "dummyperson@test.pt",
                SenderContactName = "dummy person",
                SenderContactPhoneNumber = "912341234",
                SenderName = "dummy person",
                BarCode = barcode,
                TotalWeightOfVolumes = totalWeight,
                Packages = new List<Package>()
                {
                    new Package
                    {
                        Height = 2,
                        Length = 2,
                        PackageBarCode = $"{barcode}001",
                        PackageNumber = 1,
                        Weight = totalWeight / 2,
                        Width = 2
                    },
                    new Package
                    {
                        Height = 2,
                        Length = 2,
                        PackageBarCode = $"{barcode}002",
                        PackageNumber = 2,
                        Weight = totalWeight / 2,
                        Width = 2
                    }
                }
            };
        }
    }
}
