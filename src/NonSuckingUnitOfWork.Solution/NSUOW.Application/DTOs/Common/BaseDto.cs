namespace NSUOW.Application.DTOs.Common
{
    public abstract class BaseDto
    {
        public int Id { get; set; }
        public DateTime CreatedDateUtc { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime? UpdatedDateUtc { get; set; }
        public string? UpdatedBy { get; set; }
    }
}