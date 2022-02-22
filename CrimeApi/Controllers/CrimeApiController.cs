using AutoMapper;
using CrimeApi.Models;
using CrimeApi.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
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
        if (!ObjectId.TryParse(id, out _))
        {
            _logger.LogInformation($"Failed to find the crime event with invalid id={id}");
            return BadRequest("Invalid id format");
        }

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
        if (! await _crimeEventRepository.IsExistingCrimeEventTypeAsync(crimeEventCreateDto.CrimeEventType))
        {
            _logger.LogInformation($"Failed to create new crime event with invalid crimeEventType={crimeEventCreateDto.CrimeEventType}");
            return BadRequest("Invalid crimeEventType");
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

    [HttpPost("types")]
    [SwaggerOperation("Creates a new crime event type", "POST crimes/types")]
    public async Task<ActionResult> CreateCrimeEventType(CrimeEventTypeCreateDto crimeEventTypeCreateDto)
    {
        if (await _crimeEventRepository.IsExistingCrimeEventTypeAsync(crimeEventTypeCreateDto.EventType))
        {
            _logger.LogInformation($"Failed to create a new crime event type because it already exists ({crimeEventTypeCreateDto.EventType})");
            return BadRequest("Crime event type already exists");
        }
        var newCrimeEventType = await _crimeEventRepository.CreateCrimeEventTypeAsync(_mapper.Map<CrimeEventType>(crimeEventTypeCreateDto));
        _logger.LogInformation($"Created a new crime event type ({newCrimeEventType})");
        return Ok();
    }
}
