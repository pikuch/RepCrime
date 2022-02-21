using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RepCrimeCommon.Dtos;

namespace CrimeApi.Controllers;

[ApiController]
[Route("crimes")]
public class CrimeApiController : ControllerBase
{
    private readonly ILogger<CrimeApiController> _logger;
    private readonly IMapper _mapper;

    public CrimeApiController(
        ILogger<CrimeApiController> logger,
        IMapper mapper)
    {
        _logger = logger;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CrimeEventReadDto>>> GetAllCrimeEvents()
    {
        throw new NotImplementedException();
    }
}
