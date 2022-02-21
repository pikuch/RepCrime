using AutoMapper;
using CrimeApi.Models;
using RepCrimeCommon.Dtos;

namespace CrimeApi.Data;

public class AutomapperProfile : Profile
{
    public AutomapperProfile()
    {
        CreateMap<CrimeEventCreateDto, CrimeEvent>();
        CreateMap<CrimeEvent, CrimeEventReadDto>();
        CreateMap<CrimeEventTypeCreateDto, CrimeEventType>();
        CreateMap<CrimeEventType, CrimeEventTypeReadDto>();
    }
}
