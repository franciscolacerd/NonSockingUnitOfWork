using NSUOW.Application.Extensions;
using NSUOW.Domain;
using System;
using System.Collections.Generic;

namespace NSUOW.Persistence.Tests.Common
{
    public class BaseTest
    {
        protected static Service GetDummyService(string serviceBarCode)
        {
            return new Service
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
                ServiceBarCode = serviceBarCode,
                TotalWeightOfVolumes = 16.9m,
                CreatedBy = "Unit Test",
                Volumes = new List<Volume>()
                {
                    new Volume
                    {
                        Height = 2,
                        Length = 2,
                        VolumeBarCode = $"{serviceBarCode}001",
                        VolumeNumber = 1,
                        Weight = 8.45m,
                        Width = 2,
                        CreatedBy = "Unit Test"
                    },
                    new Volume
                    {
                        Height = 2,
                        Length = 2,
                        VolumeBarCode = $"{serviceBarCode}002",
                        VolumeNumber = 2,
                        Weight = 8.45m,
                        Width = 2,
                        CreatedBy = "Unit Test",
                    }
                }
            };
        }
    }
}
