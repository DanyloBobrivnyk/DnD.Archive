using AutoMapper;
using DnD.Archive.Api.DTOs.Request;
using DnD.Archive.Api.DTOs.Response;
using DnD.Archive.Api.Models;

namespace DnD.Archive.Api.Helpers.Automapper
{
    public class DnDArchiveMappingProfile : Profile
    {
        public DnDArchiveMappingProfile()
        {
            CreateMap<Character, CharacterDTOResponse>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Bio, opt => opt.MapFrom(src => src.Bio))
                .ForMember(dest => dest.Manapool, opt => opt.MapFrom(src => src.Manapool))
                .ForMember(dest => dest.Hitpoints, opt => opt.MapFrom(src => src.Hitpoints))
                .ForMember(dest => dest.SpecializationName, opt => opt.MapFrom(src => src.SpecializationName));

            CreateMap<CharacterDTORequest, Character>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Bio, opt => opt.MapFrom(src => src.Bio))
                .ForMember(dest => dest.Manapool, opt => opt.MapFrom(src => src.Manapool))
                .ForMember(dest => dest.Hitpoints, opt => opt.MapFrom(src => src.Hitpoints))
                .ForMember(dest => dest.SpecializationName, opt => opt.MapFrom(src => src.SpecializationName));

            CreateMap<Skill, SkillDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Manacost, opt => opt.MapFrom(src => src.Manacost))
                .ForMember(dest => dest.HealthImpact, opt => opt.MapFrom(src => src.HealthImpact));

            CreateMap<Specialization, SpecializationDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
        }
    }
}
