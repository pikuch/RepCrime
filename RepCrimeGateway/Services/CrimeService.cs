using RepCrimeCommon.Dtos;
using RepCrimeCommon.Models;
using System.Collections.Specialized;
using System.Web;

namespace RepCrimeGateway.Services;

public class CrimeService : ICrimeService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public CrimeService(
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }

    public async Task<bool> AssignOfficerAsync(string id, string officerCodename)
    {
        var httpClient = _httpClientFactory.CreateClient();
        HttpRequestMessage request = new HttpRequestMessage(
            HttpMethod.Put,
            $"{_configuration["CrimeService"]}/crimes/{id}/officer/{officerCodename}");
        var response = await httpClient.SendAsync(request);
        return response.IsSuccessStatusCode;
    }

    public async Task<CrimeEventReadDto?> CreateCrimeEventAsync(CrimeEventCreateDto crimeEventCreateDto)
    {
        var httpClient = _httpClientFactory.CreateClient();
        HttpRequestMessage request = new HttpRequestMessage(
            HttpMethod.Post,
            $"{_configuration["CrimeService"]}/crimes");
        request.Content = JsonContent.Create(crimeEventCreateDto);
        
        var response = await httpClient.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsAsync<CrimeEventReadDto>();
        }
        return null;
    }

    public async Task<CrimeEventTypeReadDto?> CreateCrimeEventTypeAsync(CrimeEventTypeCreateDto crimeEventTypeCreateDto)
    {
        var httpClient = _httpClientFactory.CreateClient();
        HttpRequestMessage request = new HttpRequestMessage(
            HttpMethod.Post,
            $"{_configuration["CrimeService"]}/crimes");
        request.Content = JsonContent.Create(crimeEventTypeCreateDto);

        var response = await httpClient.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsAsync<CrimeEventTypeReadDto>();
        }
        return null;
    }

    public async Task<CrimeEventReadDto?> GetCrimeEventByIdAsync(string id)
    {
        var httpClient = _httpClientFactory.CreateClient();
        HttpRequestMessage request = new HttpRequestMessage(
            HttpMethod.Get,
            $"{_configuration["CrimeService"]}/crimes/{id}");
        
        var response = await httpClient.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsAsync<CrimeEventReadDto>();
        }
        return null;
    }

    public async Task<IEnumerable<CrimeEventReadDto>?> GetCrimeEventsAsync(QueryParameters queryParameters)
    {
        var httpClient = _httpClientFactory.CreateClient();
        var url = new UriBuilder($"{_configuration["CrimeService"]}/crimes");
        NameValueCollection queryParams = HttpUtility.ParseQueryString(url.Query);
        queryParams.Add("StartDate", queryParameters.StartDate.ToString());
        queryParams.Add("StopDate", queryParameters.StopDate.ToString());
        queryParams.Add("Descending", queryParameters.Descending.ToString());
        queryParams.Add("PageNumber", queryParameters.PageNumber.ToString());
        queryParams.Add("PageSize", queryParameters.PageSize.ToString());
        url.Query = queryParams.ToString();
        HttpRequestMessage request = new HttpRequestMessage(
            HttpMethod.Get,
            url.ToString());

        var response = await httpClient.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsAsync<IEnumerable<CrimeEventReadDto>>();
        }
        return null;
    }

    public async Task<IEnumerable<CrimeEventTypeReadDto>?> GetCrimeEventTypesAsync()
    {
        var httpClient = _httpClientFactory.CreateClient();
        HttpRequestMessage request = new HttpRequestMessage(
            HttpMethod.Get,
            $"{_configuration["CrimeService"]}/crimes/types");

        var response = await httpClient.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsAsync<IEnumerable<CrimeEventTypeReadDto>>();
        }
        return null;
    }
}
