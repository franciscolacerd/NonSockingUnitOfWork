using NSUOW.Application.DTOs.Common;

namespace NSUOW.Application.DTOs
{
    public partial class PackageDto : BaseDto
    {
        public int DeliveryId { get; set; }
        public string PackageBarCode { get; set; } = null!;
        public int PackageNumber { get; set; }
        public decimal Weight { get; set; }
        public decimal? Height { get; set; }
        public decimal? Length { get; set; }
        public decimal? Width { get; set; }
        public virtual DeliveryDto Delivery { get; set; } = null!;
    }
}
