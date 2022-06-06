using NSUOW.Domain.Common;

namespace NSUOW.Domain
{
    public partial class Volume : BaseDomainEntity
    {
        public int ServiceId { get; set; }
        public string VolumeBarCode { get; set; } = null!;
        public int VolumeNumber { get; set; }
        public decimal Weight { get; set; }
        public decimal? Height { get; set; }
        public decimal? Length { get; set; }
        public decimal? Width { get; set; }
        public virtual Service Service { get; set; } = null!;
    }
}
