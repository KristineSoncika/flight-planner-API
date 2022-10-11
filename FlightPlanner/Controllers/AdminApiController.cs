using FlightPlanner.Context;
using FlightPlanner.Models;
using FlightPlanner.Repositories;
using FlightPlanner.Validations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers;

[Route("admin-api/flights")]
[ApiController, Authorize]
public class AdminApiController : ControllerBase
{
    private readonly FlightRepository _flightRepository;

    public AdminApiController(FlightRepository flightRepository)
    {
        _flightRepository = flightRepository;
    }
    
    [Route("{id:int}")]
    [HttpGet]
    public IActionResult GetFlights(int id)
    {
        var flight = _flightRepository.GetFlight(id);

        if (flight == null)
        {
            return NotFound();
        }

        return Ok(flight);
    }
    
    [HttpPut]
    public IActionResult AddFlights(AddFlight request)
    {
        if (AdminApiValidator.HasInvalidValues(request) || 
            AdminApiValidator.IsWrongDate(request) || 
            AdminApiValidator.IsSameAirport(request))
        {
            return  BadRequest();
        }

        if (_flightRepository.FlightAlreadyExists(request))
        {
            return Conflict();
        }

        var flight = _flightRepository.AddFlight(request);
        return Created( "", flight);
    }
    
    [Route("{id:int}")]
    [HttpDelete]
    public IActionResult DeleteFlights(int id)
    {
        var flight = _flightRepository.GetFlight(id);
        
        if (flight == null)
        {
            return Ok();
        }
        
        _flightRepository.DeleteFlight(id);
        return Ok();
    }
}