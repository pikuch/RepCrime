using AutoMapper;
using LawEnforcementApi.Models;
using LawEnforcementApi.Services;
using Microsoft.AspNetCore.Mvc;
using RepCrimeCommon.Dtos;
using Swashbuckle.AspNetCore.Annotations;

namespace LawEnforcementApi.Controllers;
[ApiController]
[Route("officers")]
public class LawEnforcementApiController : ControllerBase
{
    private readonly ILogger<LawEnforcementApiController> _logger;
    private readonly IMapper _mapper;
    private readonly ILawEnforcementRepository _lawEnforcementRepository;

    public LawEnforcementApiController(
        ILogger<LawEnforcementApiController> logger,
        IMapper mapper,
        ILawEnforcementRepository lawEnforcementRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _lawEnforcementRepository = lawEnforcementRepository;
    }

    [HttpGet]
    [SwaggerOperation("Gets all officers", "GET officers")]
    public async Task<ActionResult<IEnumerable<LawEnforcementOfficerReadDto>>> GetAllOfficers()
    {
        var officers = await _lawEnforcementRepository.GetAllOfficersAsync();
        if (officers.Any())
        {
            _logger.LogInformation($"Returned {officers.Count()} officers", officers);
            return Ok(_mapper.Map<IEnumerable<LawEnforcementOfficerReadDto>>(officers));
        }
        _logger.LogInformation($"Failed to find any officers");
        return NotFound("No officers found");
    }

    [HttpGet("{codename}", Name = "GetOfficerByCodename")]
    [SwaggerOperation("Gets officer by codename", "GET officers/{codename}")]
    public async Task<ActionResult<LawEnforcementOfficerReadDto>> GetOfficerByCodename(string codename)
    {
        var officer = await _lawEnforcementRepository.GetOfficerByCodenameAsync(codename);
        if (officer != null)
        {
            _logger.LogInformation($"Returned officer with codename {codename}", officer);
            return Ok(_mapper.Map<LawEnforcementOfficerReadDto>(officer));
        }
        _logger.LogInformation($"Failed to find officer with codename {codename}");
        return NotFound($"No officers with codename {codename} found");
    }

    [HttpPost]
    [SwaggerOperation("Creates a new officer", "POST officers")]
    public async Task<ActionResult<LawEnforcementOfficerReadDto>> CreateOfficer(LawEnforcementOfficerCreateDto officerCreateDto)
    {
        var ranks = await _lawEnforcementRepository.GetAllRanksAsync();
        if (!ranks.Any(x => x.Id == officerCreateDto.RankId))
        {
            _logger.LogInformation($"Failed to create new officer with rankId {officerCreateDto.RankId}", officerCreateDto);
            return BadRequest("Invalid rank id");
        }

        var officerWithCodename = await _lawEnforcementRepository.GetOfficerByCodenameAsync(officerCreateDto.Codename);
        if (officerWithCodename != null)
        {
            _logger.LogInformation($"Failed to create new officer with codename {officerCreateDto.Codename}", officerCreateDto);
            return BadRequest("Codename already in use");
        }

        await _lawEnforcementRepository.AddNewOfficerAsync(_mapper.Map<LawEnforcementOfficer>(officerCreateDto));

        _logger.LogInformation($"Created officer with codename {officerCreateDto.Codename}", officerCreateDto);
        return CreatedAtRoute(nameof(GetOfficerByCodename), new { officerCreateDto.Codename }, officerCreateDto);
    }

    [HttpGet("ranks")]
    [SwaggerOperation("Gets all ranks", "GET officers/ranks")]
    public async Task<ActionResult<IEnumerable<RankReadDto>>> GetAllRanks()
    {
        var ranks = await _lawEnforcementRepository.GetAllRanksAsync();
        if (ranks.Any())
        {
            _logger.LogInformation($"Returned {ranks.Count()} ranks", ranks);
            return Ok(_mapper.Map<IEnumerable<RankReadDto>>(ranks));
        }
        _logger.LogInformation($"Failed to find any ranks");
        return NotFound("No ranks found");
    }

    [HttpPost("ranks")]
    [SwaggerOperation("Create a new rank", "POST officers/ranks")]
    public async Task<ActionResult> CreateNewRank(RankCreateDto rankCreateDto)
    {
        var ranks = await _lawEnforcementRepository.GetAllRanksAsync();
        if (ranks.Any(x => x.Name == rankCreateDto.Name))
        {
            _logger.LogInformation($"Failed to create a new rank {rankCreateDto.Name} because of a name conflict");
            return BadRequest("Rank already exists");
        }
        var newRank = await _lawEnforcementRepository.AddNewRankAsync(_mapper.Map<Rank>(rankCreateDto));

        _logger.LogInformation($"Created a new rank {newRank.Name}", newRank);
        return Ok($"Rank created with id={newRank.Id}");
    }
}
