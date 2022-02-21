using CrimeApi.Models;
using RepCrimeCommon.Models;

namespace CrimeApi.Services;

public interface ICrimeEventRepository
{
    public Task<IEnumerable<CrimeEvent>> GetCrimeEventsAsync(QueryParameters queryParameters);
    public Task<CrimeEvent?> GetCrimeEventByIdAsync(string id);
    public Task<CrimeEvent> CreateCrimeEventAsync(CrimeEvent crimeEvent);
    public Task<bool> IsValidCrimeEventIdAsync(string crimeEventTypeId);
    public Task<IEnumerable<CrimeEventType>> GetCrimeEventTypesAsync();
    public Task<CrimeEventType?> GetCrimeEventTypeByIdAsync(string id);
    public Task<CrimeEventType> CreateCrimeEventTypeAsync(CrimeEventType crimeEventType);
}
