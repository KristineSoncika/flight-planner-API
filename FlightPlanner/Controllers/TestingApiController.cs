using FlightPlanner.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers;

[Route("testing-api/clear")]
[ApiController]
public class TestingApiController : ControllerBase
{
    private readonly IFlightService _flightService;

    public TestingApiController(IFlightService flightService)
    {
        _flightService = flightService;
    }
    
    [HttpPost]
    public IActionResult Clear()
    {
        _flightService.ClearDatabase();
        return Ok();
    }
}