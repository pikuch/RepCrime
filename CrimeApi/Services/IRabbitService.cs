using RepCrimeCommon.Dtos;

namespace CrimeApi.Services;

public interface IRabbitService
{
    public bool SendMessage(CrimeEventReadDto crimeEvent);
}
