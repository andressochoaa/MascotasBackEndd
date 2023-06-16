using AutoMapper;
using MascotasBackEnd.Models.Dto;

namespace MascotasBackEnd.Models.Profiles
{
    public class MascotaProfile: Profile
    {
        public MascotaProfile()
        {
            CreateMap<Mascota, MascotaDto>();
            CreateMap<MascotaDto, Mascota>();
        }
    }
}
