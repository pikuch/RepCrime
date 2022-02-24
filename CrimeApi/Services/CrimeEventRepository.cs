using CrimeApi.Models;
using MongoDB.Driver;
using RepCrimeCommon.Models;

namespace CrimeApi.Services;

public class CrimeEventRepository : ICrimeEventRepository
{
    private readonly MongoClient _mongoClient;
    private readonly IMongoDatabase _crimeDatabase;
    private readonly IMongoCollection<CrimeEvent> _crimeEventCollection;
    private readonly IMongoCollection<CrimeEventType> _crimeEventTypeCollection;
    public CrimeEventRepository(IConfiguration configuration)
    {
        MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(configuration["MongoConnectionString"]));
        _mongoClient = new MongoClient(settings);
        _crimeDatabase = _mongoClient.GetDatabase(configuration["DatabaseName"]);
        _crimeEventCollection = _crimeDatabase.GetCollection<CrimeEvent>(configuration["CrimeEventCollectionName"]);
        _crimeEventTypeCollection = _crimeDatabase.GetCollection<CrimeEventType>(configuration["CrimeEventTypeCollectionName"]);
    }

    public async Task AssignOfficerAsync(string id, string officerCodename)
    {
        await _crimeEventCollection.UpdateOneAsync(
            Builders<CrimeEvent>.Filter.Eq(x => x.Id, id),
            Builders<CrimeEvent>.Update.Set(x => x.AssignedLawEnforcementId, officerCodename));
    }

    public async Task<CrimeEvent> CreateCrimeEventAsync(CrimeEvent crimeEvent)
    {
        await _crimeEventCollection.InsertOneAsync(crimeEvent);
        return crimeEvent;
    }

    public async Task<CrimeEventType> CreateCrimeEventTypeAsync(CrimeEventType crimeEventType)
    {
        await _crimeEventTypeCollection.InsertOneAsync(crimeEventType);
        return crimeEventType;
    }

    public async Task<CrimeEvent?> GetCrimeEventByIdAsync(string id)
    {
        return await _crimeEventCollection.Find(x => x.Id == id).SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<CrimeEvent>> GetCrimeEventsAsync(QueryParameters queryParameters)
    {
        var crimeEvents = await _crimeEventCollection
            .Find(x => x.Date.CompareTo(queryParameters.StartDate) >= 0 && x.Date.CompareTo(queryParameters.StopDate) <= 0)
            .ToListAsync();
        var sortedCrimeEvents = queryParameters.Descending
            ? crimeEvents.OrderByDescending(x => x.Date)
            : crimeEvents.OrderBy(x => x.Date);

        return sortedCrimeEvents
            .Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize)
            .Take(queryParameters.PageSize);
    }

    public async Task<IEnumerable<CrimeEventType>> GetCrimeEventTypesAsync()
    {
        return await _crimeEventTypeCollection.Find(_ => true).ToListAsync();
    }

    public async Task<bool> IsExistingCrimeEventTypeAsync(string crimeEventType)
    {
        var foundCrimeEventType = await _crimeEventTypeCollection.Find(x => x.EventType == crimeEventType).SingleOrDefaultAsync();
        return foundCrimeEventType != null;
    }
}
