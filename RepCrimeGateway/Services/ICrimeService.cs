using RepCrimeCommon.Dtos;
using RepCrimeCommon.Models;

namespace RepCrimeGateway.Services;

public interface ICrimeService
{
    public Task<IEnumerable<CrimeEventReadDto>?> GetCrimeEventsAsync(QueryParameters queryParameters);
    public Task<CrimeEventReadDto?> GetCrimeEventByIdAsync(string id);
    public Task<CrimeEventReadDto?> CreateCrimeEventAsync(CrimeEventCreateDto crimeEventCreateDto);
    public Task<IEnumerable<CrimeEventTypeReadDto>?> GetCrimeEventTypesAsync();
    public Task<CrimeEventTypeReadDto?> CreateCrimeEventTypeAsync(CrimeEventTypeCreateDto crimeEventTypeCreateDto);
    public Task<bool> AssignOfficerAsync(string id, string officerCodename);
}
