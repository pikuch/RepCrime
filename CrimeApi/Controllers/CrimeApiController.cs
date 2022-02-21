using AutoMapper;
using CrimeApi.Models;
using CrimeApi.Services;
using Microsoft.AspNetCore.Mvc;
using RepCrimeCommon.Dtos;
using RepCrimeCommon.Enums;
using RepCrimeCommon.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace CrimeApi.Controllers;

[ApiController]
[Route("crimes")]
public class CrimeApiController : ControllerBase
{
    private readonly ILogger<CrimeApiController> _logger;
    private readonly IMapper _mapper;
    private readonly ICrimeEventRepository _crimeEventRepository;

    public CrimeApiController(
        ILogger<CrimeApiController> logger,
        IMapper mapper,
        ICrimeEventRepository crimeEventRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _crimeEventRepository = crimeEventRepository;
    }

    [HttpGet]
    [SwaggerOperation("Gets filtered, sorted and paginated crime events", "GET crimes")]
    public async Task<ActionResult<IEnumerable<CrimeEventReadDto>>> GetAllCrimeEvents([FromQuery] QueryParameters queryParameters)
    {
        var crimeEvents = await _crimeEventRepository.GetCrimeEventsAsync(queryParameters);
        if (crimeEvents.Any())
        {
            _logger.LogInformation($"Returned {crimeEvents.Count()} crime events", crimeEvents);
            return Ok(_mapper.Map<IEnumerable<CrimeEventReadDto>>(crimeEvents));
        }
        _logger.LogInformation($"Failed to find crime events matching requested parameters", queryParameters);
        return NotFound();
    }

    [HttpGet("{id}", Name = "GetCrimeEventById")]
    [SwaggerOperation("Gets crime event by id", "GET crimes/{id}")]
    public async Task<ActionResult<CrimeEventReadDto>> GetCrimeEventById(string id)
    {
        var crimeEvent = await _crimeEventRepository.GetCrimeEventByIdAsync(id);
        if (crimeEvent == null)
        {
            _logger.LogInformation($"Failed to find the crime event with id={id}");
            return NotFound();
        }
        _logger.LogInformation($"Returned crime event with id={id}", crimeEvent);
        return Ok(_mapper.Map<CrimeEventReadDto>(crimeEvent));
    }

    [HttpPost]
    [SwaggerOperation("Creates a new crime event", "POST crimes")]
    public async Task<ActionResult<CrimeEventReadDto>> CreateCrimeEvent(CrimeEventCreateDto crimeEventCreateDto)
    {
        if (! await _crimeEventRepository.IsValidCrimeEventIdAsync(crimeEventCreateDto.CrimeEventTypeId))
        {
            _logger.LogInformation($"Failed to create new crime event with invalid crimeEventTypeId={crimeEventCreateDto.CrimeEventTypeId}");
            return BadRequest("Invalid crimeEventTypeId");
        }
        var crimeEvent = _mapper.Map<CrimeEvent>(crimeEventCreateDto);
        crimeEvent.Status = CrimeEventStatus.Waiting;

        var newCrimeEvent = await _crimeEventRepository.CreateCrimeEventAsync(crimeEvent);
        _logger.LogInformation($"Created a new crime event with id={newCrimeEvent.Id}");
        return CreatedAtRoute(
            nameof(GetCrimeEventById),
            new { id = newCrimeEvent.Id },
            _mapper.Map<CrimeEventReadDto>(newCrimeEvent));
    }

    [HttpGet("types")]
    [SwaggerOperation("Gets all crime event types", "GET crimes/types")]
    public async Task<ActionResult<IEnumerable<CrimeEventTypeReadDto>>> GetAllCrimeEventTypes()
    {
        var crimeEventTypes = await _crimeEventRepository.GetCrimeEventTypesAsync();
        if (crimeEventTypes.Any())
        {
            _logger.LogInformation($"Returned {crimeEventTypes.Count()} crime event types", crimeEventTypes);
            return Ok(_mapper.Map<IEnumerable<CrimeEventTypeReadDto>>(crimeEventTypes));
        }
        _logger.LogInformation($"Failed to find any crime event types");
        return NotFound();
    }

    [HttpGet("types/{id}", Name = "GetCrimeEventTypeById")]
    [SwaggerOperation("Gets crime event type by id", "GET crimes/types/{id}")]
    public async Task<ActionResult<CrimeEventTypeReadDto>> GetCrimeEventTypeById(string id)
    {
        var crimeEventType = await _crimeEventRepository.GetCrimeEventTypeByIdAsync(id);
        if (crimeEventType == null)
        {
            _logger.LogInformation($"Failed to find the crime event type with id={id}");
            return NotFound();
        }
        _logger.LogInformation($"Returned crime event type with id={id}", crimeEventType);
        return Ok(_mapper.Map<CrimeEventTypeReadDto>(crimeEventType));
    }

    [HttpPost("types")]
    [SwaggerOperation("Creates a new crime event type", "POST crimes/types")]
    public async Task<ActionResult> CreateCrimeEventType(CrimeEventTypeCreateDto crimeEventTypeCreateDto)
    {
        var newCrimeEventType = await _crimeEventRepository.CreateCrimeEventTypeAsync(_mapper.Map<CrimeEventType>(crimeEventTypeCreateDto));
        _logger.LogInformation($"Created a new crime event type with id={newCrimeEventType.Id}");
        return CreatedAtRoute(
            nameof(GetCrimeEventTypeById),
            new { id = newCrimeEventType.Id },
            _mapper.Map<CrimeEventTypeReadDto>(newCrimeEventType));
    }
}
