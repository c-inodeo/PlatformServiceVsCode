using AutoMapper;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Profiles
{
    public class PlatformProfile : Profile
    {
        public PlatformProfile()
        {
            // Syntax: <Source -> Target>
            CreateMap<Platform, PlatformReadDto>();
            CreateMap<PlatformCreateDto, Platform>();
            CreateMap<PlatformReadDto, PlatformPublishedDto>();
            CreateMap<Platform, GrpcPlatformModel>() //mapped from Protos/platforms.proto
                .ForMember(dest => dest.PlatformId, opt=> opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt=> opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Publisher, opt=> opt.MapFrom(src => src.Publisher));
        }
    }
}