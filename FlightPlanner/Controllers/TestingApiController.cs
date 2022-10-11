using FlightPlanner.Context;
using FlightPlanner.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers;

[Route("testing-api/clear")]
[ApiController]
public class TestingApiController : ControllerBase
{
    private readonly FlightRepository _flightRepository;

    public TestingApiController(FlightRepository flightRepository)
    {
        _flightRepository = flightRepository;
    }
    
    [HttpPost]
    public IActionResult Clear()
    {
        _flightRepository.Clear();
        return Ok();
    }
}