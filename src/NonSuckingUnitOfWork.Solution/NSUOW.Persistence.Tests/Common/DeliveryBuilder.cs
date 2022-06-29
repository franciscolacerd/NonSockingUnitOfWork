using NSUOW.Application.Extensions;
using NSUOW.Domain;
using System;
using System.Collections.Generic;

namespace NSUOW.Persistence.Tests.Common
{
    public class DeliveryBuilder
    {
        private Delivery _delivery = new Delivery();
        private string _barcode;
        private decimal _totalWeight;

        public DeliveryBuilder()
        {
        }

        public DeliveryBuilder WithBarcode(string barcode)
        {
            this._barcode = barcode;
            return this;
        }

        public DeliveryBuilder WithWeight(decimal totalWeight)
        {
            this._totalWeight = totalWeight;
            return this;
        }

        public DeliveryBuilder WithOneDelivery()
        {
            _delivery = new Delivery
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
                BarCode = this._barcode,
                TotalWeightOfVolumes = this._totalWeight
            };
            return this;
        }

        public DeliveryBuilder WithTwoPackages()
        {
            _delivery.Packages = new List<Package>()
                {
                    new Package
                    {
                        Height = 2,
                        Length = 2,
                        PackageBarCode = $"{this._barcode}001",
                        PackageNumber = 1,
                        Weight = this._totalWeight / 2,
                        Width = 2
                    },
                    new Package
                    {
                        Height = 2,
                        Length = 2,
                        PackageBarCode = $"{this._barcode}002",
                        PackageNumber = 2,
                        Weight = this._totalWeight / 2,
                        Width = 2
                    }
                };
            return this;
        }

        public Delivery Build()
        {
            return _delivery;
        }
    }
}