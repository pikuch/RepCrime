using CrimeApi.Models;
using RepCrimeCommon.Models;

namespace CrimeApi.Services;

public interface ICrimeEventRepository
{
    public Task<IEnumerable<CrimeEvent>> GetCrimeEventsAsync(QueryParameters queryParameters);
    public Task<CrimeEvent?> GetCrimeEventByIdAsync(string id);
    public Task<CrimeEvent> CreateCrimeEventAsync(CrimeEvent crimeEvent);
    public Task<bool> IsExistingCrimeEventTypeAsync(string crimeEventType);
    public Task<IEnumerable<CrimeEventType>> GetCrimeEventTypesAsync();
    public Task<CrimeEventType> CreateCrimeEventTypeAsync(CrimeEventType crimeEventType);
    public Task AssignOfficerAsync(string id, string officerCodename);
}
