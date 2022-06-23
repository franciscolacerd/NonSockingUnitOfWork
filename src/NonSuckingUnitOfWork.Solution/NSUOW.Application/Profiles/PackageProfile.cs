using NSUOW.Application.DTOs;
using NSUOW.Domain;

namespace NSUOW.Application.Profiles
{
    public partial class PackageProfile : AutoMapper.Profile
    {
        public PackageProfile()
        {
            CreateMap<Package, PackageDto>().ReverseMap();
        }
    }
}
