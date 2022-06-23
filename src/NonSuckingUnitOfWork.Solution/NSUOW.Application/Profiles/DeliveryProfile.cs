using NSUOW.Application.DTOs;
using NSUOW.Domain;

namespace NSUOW.Application.Profiles
{
    public partial class DeliveryProfile : AutoMapper.Profile
    {
        public DeliveryProfile()
        {
            CreateMap<Delivery, DeliveryDto>().ReverseMap();
        }
    }
}
