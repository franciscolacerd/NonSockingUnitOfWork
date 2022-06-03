using NSUOW.Application.DTOs.Common;

namespace NSUOW.Application.DTOs
{
    public partial class VolumeDto : BaseDto
    {
        public long ServiceId { get; set; }
        public string VolumeBarCode { get; set; } = null!;
        public int VolumeNumber { get; set; }
        public decimal Weight { get; set; }
        public decimal? Height { get; set; }
        public decimal? Length { get; set; }
        public decimal? Width { get; set; }
        public virtual ServiceDto Service { get; set; } = null!;
    }
}
