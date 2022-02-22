using AutoMapper;
using LawEnforcementApi.Models;
using RepCrimeCommon.Dtos;

namespace LawEnforcementApi.Data;

public class AutomapperProfile : Profile
{
    public AutomapperProfile()
    {
        CreateMap<RankCreateDto, Rank>();
        CreateMap<Rank, RankReadDto>();
        CreateMap<LawEnforcementOfficerCreateDto, LawEnforcementOfficer>();
        CreateMap<LawEnforcementOfficer, LawEnforcementOfficerReadDto>()
            .ForMember(dest => dest.RankName,
               opts => opts.MapFrom(src => src.Rank.Name));
    }
}
