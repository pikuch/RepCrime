using Microsoft.AspNetCore.Mvc;

namespace CrimeFeedbackService.Controllers;
[ApiController]
[Route("[controller]")]
public class CrimeFeedbackServiceController : ControllerBase
{
    private readonly ILogger<CrimeFeedbackServiceController> _logger;

    public CrimeFeedbackServiceController(ILogger<CrimeFeedbackServiceController> logger)
    {
        _logger = logger;
    }

}
