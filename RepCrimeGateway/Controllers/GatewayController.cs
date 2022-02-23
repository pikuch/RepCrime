using Microsoft.AspNetCore.Mvc;
using RepCrimeCommon.Dtos;
using RepCrimeCommon.Models;
using RepCrimeGateway.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace RepCrimeGateway.Controllers;
[ApiController]
[Route("api")]
public class GatewayController : ControllerBase
{
    private readonly ILogger<GatewayController> _logger;
    private readonly ICrimeService _crimeService;

    public GatewayController(
        ILogger<GatewayController> logger,
        ICrimeService crimeService)
    {
        _logger = logger;
        _crimeService = crimeService;
    }

    [HttpGet("crimes")]
    [SwaggerOperation("Gets filtered, sorted and paginated crime events", "GET api/crimes")]
    public async Task<ActionResult<IEnumerable<CrimeEventReadDto>>> GetAllCrimeEvents([FromQuery] QueryParameters queryParameters)
    {
        var crimeEvents = await _crimeService.GetCrimeEventsAsync(queryParameters);
        if (crimeEvents == null)
        {
            _logger.LogInformation($"Failed to find crime events matching requested parameters", queryParameters);
            return NotFound("No crime events matching requested parameters found");
        }

        _logger.LogInformation($"Returned {crimeEvents.Count()} crime events", crimeEvents);
        return Ok(crimeEvents);
    }

    [HttpGet("crimes/{id}", Name = "GetCrimeEventById")]
    [SwaggerOperation("Gets crime event by id", "GET api/crimes/{id}")]
    public async Task<ActionResult<CrimeEventReadDto>> GetCrimeEventById(string id)
    {
        var crimeEvent = await _crimeService.GetCrimeEventByIdAsync(id);

        if (crimeEvent == null)
        {
            _logger.LogInformation($"Failed to find the crime event with id={id}");
            return NotFound("No crime events matching requested id found");
        }

        _logger.LogInformation($"Returned crime event with id={id}", crimeEvent);
        return Ok(crimeEvent);
    }

    [HttpPost("crimes")]
    [SwaggerOperation("Creates a new crime event", "POST api/crimes")]
    public async Task<ActionResult<CrimeEventReadDto>> CreateCrimeEvent(CrimeEventCreateDto crimeEventCreateDto)
    {
        var newCrimeEvent = await _crimeService.CreateCrimeEventAsync(crimeEventCreateDto);
        if (newCrimeEvent == null)
        {
            _logger.LogInformation($"Failed to create a new crime event");
            return BadRequest();
        }

        _logger.LogInformation($"Created a new crime event with id={newCrimeEvent.Id}");
        return CreatedAtRoute(
            nameof(GetCrimeEventById),
            new { id = newCrimeEvent.Id },
            newCrimeEvent);
    }

    [HttpGet("crimes/types")]
    [SwaggerOperation("Gets all crime event types", "GET api/crimes/types")]
    public async Task<ActionResult<IEnumerable<CrimeEventTypeReadDto>>> GetAllCrimeEventTypes()
    {
        var crimeEventTypes = await _crimeService.GetCrimeEventTypesAsync();
        if (crimeEventTypes == null)
        {
            _logger.LogInformation($"Failed to find any crime event types");
            return NotFound();
        }
        
        _logger.LogInformation($"Returned {crimeEventTypes.Count()} crime event types", crimeEventTypes);
        return Ok(crimeEventTypes);
    }

    [HttpPost("crimes/types")]
    [SwaggerOperation("Creates a new crime event type", "POST api/crimes/types")]
    public async Task<ActionResult> CreateCrimeEventType(CrimeEventTypeCreateDto crimeEventTypeCreateDto)
    {
        var newCrimeEventType = await _crimeService.CreateCrimeEventTypeAsync(crimeEventTypeCreateDto);
        if (newCrimeEventType == null)
        {
            _logger.LogInformation($"Failed to create a new crime event type");
            return BadRequest();
        }

        _logger.LogInformation($"Created a new crime event type ({newCrimeEventType})");
        return Ok();
    }

    [HttpPut("crimes/{id}/officer/{officerCodename}")]
    [SwaggerOperation("Assigns an officer to the crime event", "PUT api/crimes/{id}/officer/{officerCodename}")]
    public async Task<ActionResult> AssignOfficerToCrimeEvent(string id, string officerCodename)
    {
        var result = await _crimeService.AssignOfficerAsync(id, officerCodename);
        if (!result)
        {
            _logger.LogInformation($"Failed to assign the officer");
            return BadRequest();
        }
        _logger.LogInformation($"Assigned an officer with codename={officerCodename} to the crime event with id={id}");
        return Ok("Officer assigned");
    }
}
