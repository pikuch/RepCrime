using RepCrimeCommon.Dtos;

namespace CrimeApi.Services;

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

    public async Task<LawEnforcementOfficerReadDto?> GetOfficerByCodenameAsync(string officerCodename)
    {
        var httpClient = _httpClientFactory.CreateClient();
        HttpRequestMessage request = new HttpRequestMessage(
            HttpMethod.Get,
            $"{_configuration["LawEnforcementService"]}/officers/{officerCodename}");
        var response = await httpClient.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            var officer = await response.Content.ReadAsAsync<LawEnforcementOfficerReadDto>();
            return officer;
        }
        return null;
    }
}
