using RepCrimeCommon.Dtos;

namespace CrimeApi.Services;

public interface ILawEnforcementService
{
    public Task<LawEnforcementOfficerReadDto?> GetOfficerByCodenameAsync(string officerCodename);
}
