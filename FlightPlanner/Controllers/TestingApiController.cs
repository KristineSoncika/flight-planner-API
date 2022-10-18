using FlightPlanner.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers;

[Route("testing-api/clear")]
[ApiController]
public class TestingApiController : ControllerBase
{
    private readonly IClearDb _clearDb;

    public TestingApiController(IClearDb clearDb)
    {
        _clearDb = clearDb;
    }
    
    [HttpPost]
    public IActionResult Clear()
    {
        _clearDb.ClearDatabase();
        return Ok();
    }
}