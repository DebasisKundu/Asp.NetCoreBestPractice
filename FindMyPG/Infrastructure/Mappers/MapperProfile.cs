using AutoMapper;
using FindMyPG.Core.Entities;
using FindMyPG.Models;

namespace FindMyPG.Infrastructure.Mappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<State, StateModel>();
            CreateMap<City, CityModel>()
               .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.Name));

            CreateMap<StateModelRequest, State>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.StateName));

            CreateMap<CityModelRequest, City>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CityName));

            CreateMap<ZipCode, ZipCodeModel>();

            CreateMap<PGInfoModel, PGInfo>();
            CreateMap<PGRoomModel, PGRoom>();
            CreateMap<PGPackageModel, PGPackage>();
            CreateMap<RoleModel, Role>();
            CreateMap<RegisterModel, User>();
        }
    }
}
