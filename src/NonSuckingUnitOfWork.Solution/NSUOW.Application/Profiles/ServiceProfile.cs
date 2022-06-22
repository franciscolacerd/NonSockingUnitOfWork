using NSUOW.Application.DTOs;
using NSUOW.Domain;

namespace NSUOW.Application.Profiles
{
    public partial class ServiceProfile : AutoMapper.Profile
    {
        public ServiceProfile()
        {
            CreateMap<Service, ServiceDto>().ReverseMap();
        }
    }
}
