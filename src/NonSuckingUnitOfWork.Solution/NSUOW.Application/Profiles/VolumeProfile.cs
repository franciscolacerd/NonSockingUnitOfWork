using NSUOW.Application.DTOs;
using NSUOW.Domain;

namespace NSUOW.Application.Profiles
{
    public partial class VolumeProfile : AutoMapper.Profile
    {
        public VolumeProfile()
        {
            CreateMap<Volume, VolumeDto>().ReverseMap();
        }
    }
}
