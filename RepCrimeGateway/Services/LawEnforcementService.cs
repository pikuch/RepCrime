using RepCrimeCommon.Dtos;

namespace RepCrimeGateway.Services;

public class LawEnforcementService : ILawEnforcementService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public LawEnforcementService(
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }

    public async Task<LawEnforcementOfficerReadDto?> AddNewOfficerAsync(LawEnforcementOfficerCreateDto officerCreateDto)
    {
        var httpClient = _httpClientFactory.CreateClient();
        HttpRequestMessage request = new HttpRequestMessage(
            HttpMethod.Post,
            $"{_configuration["OfficerService"]}/officers");
        request.Content = JsonContent.Create(officerCreateDto);

        var response = await httpClient.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsAsync<LawEnforcementOfficerReadDto>();
        }
        return null;
    }

    public async Task<bool> AddNewRankAsync(RankCreateDto rankCreateDto)
    {
        var httpClient = _httpClientFactory.CreateClient();
        HttpRequestMessage request = new HttpRequestMessage(
            HttpMethod.Post,
            $"{_configuration["OfficerService"]}/officers/ranks");
        request.Content = JsonContent.Create(rankCreateDto);

        var response = await httpClient.SendAsync(request);
        return (response.IsSuccessStatusCode);
    }

    public async Task<IEnumerable<LawEnforcementOfficerReadDto>?> GetAllOfficersAsync()
    {
        var httpClient = _httpClientFactory.CreateClient();
        HttpRequestMessage request = new HttpRequestMessage(
            HttpMethod.Get,
            $"{_configuration["OfficerService"]}/officers");
        
        var response = await httpClient.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsAsync<IEnumerable<LawEnforcementOfficerReadDto>>();
        }
        return null;
    }

    public async Task<IEnumerable<RankReadDto>?> GetAllRanksAsync()
    {
        var httpClient = _httpClientFactory.CreateClient();
        HttpRequestMessage request = new HttpRequestMessage(
            HttpMethod.Get,
            $"{_configuration["OfficerService"]}/officers/ranks");

        var response = await httpClient.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsAsync<IEnumerable<RankReadDto>>();
        }
        return null;
    }

    public async Task<LawEnforcementOfficerReadDto?> GetOfficerByCodenameAsync(string codename)
    {
        var httpClient = _httpClientFactory.CreateClient();
        HttpRequestMessage request = new HttpRequestMessage(
            HttpMethod.Get,
            $"{_configuration["OfficerService"]}/officers/{codename}");

        var response = await httpClient.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsAsync<LawEnforcementOfficerReadDto>();
        }
        return null;
    }
}
