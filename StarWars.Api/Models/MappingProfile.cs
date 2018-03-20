using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWars.Api.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Core.Models.Character, Character>()
                .ForMember(dest => dest.Friends, opt => opt.Ignore())
                .ForMember(dest => dest.AppearsIn, opt => opt.Ignore());

            CreateMap<Core.Models.Droid, Droid>()
                .IncludeBase<Core.Models.Character, Character>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<Core.Models.Human, Human>()
                .IncludeBase<Core.Models.Character, Character>()
                .ForMember(dest => dest.HomePlanet, opt => opt.MapFrom(src => src.HomePlanet.Name))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        }
    }
}
