using FlightPlanner.Models;
using FlightPlanner.Repositories;
using FlightPlanner.Validations;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers;

[Route("api")]
[ApiController]
public class CustomerApiController : ControllerBase
{
    private readonly FlightRepository _flightRepository;

    public CustomerApiController(FlightRepository flightRepository)
    {
        _flightRepository = flightRepository;
    }
    
    [Route("airports")]
    [HttpGet]
    public IActionResult GetAirport(string search)
    {
        var airports = _flightRepository.FindAirport(search);
        
        return Ok(airports);
    }
    
    [Route("flights/search")]
    [HttpPost]
    public IActionResult SearchFlight(FlightSearch flightSearch)
    {
        if (CustomerApiValidator.HasInvalidValues(flightSearch) ||
            CustomerApiValidator.IsSameAirport(flightSearch))
        {
            return BadRequest();
        }

        var result = _flightRepository.SearchFlight(flightSearch);
        return Ok(result);
    }
    
    [Route("flights/{id:int}")]
    [HttpGet]
    public IActionResult GetFlight(int id)
    {
        var flight = _flightRepository.GetFlight(id);
        
        if (flight == null)
        {
            return NotFound();
        }
        
        return Ok(flight);
    }
}