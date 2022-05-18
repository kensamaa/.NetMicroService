using AutoMapper;
using PlatformService.Dtos;
using PlatformService.Models;


namespace PlatformService.Profils
{
    public class PlatformsProfil:Profile
    {
        public PlatformsProfil()
        {
            //source->target
            CreateMap<Platform, PlatformReadDto>();
            CreateMap<PlatformCreatedto, Platform>();
        }
    }
}
