using AutoMapper;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Core.Validations;
using FlightPlanner.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers;

[Route("admin-api/flights")]
[ApiController, Authorize]
public class AdminApiController : ControllerBase
{
    private readonly IFlightService _flightService;
    private readonly IEnumerable<IFlightValidator> _flightValidators;
    private readonly IEnumerable<IAirportValidator> _airportValidators;
    private readonly IMapper _mapper;

    public AdminApiController(IFlightService flightService, 
        IEnumerable<IFlightValidator> flightValidators, 
        IEnumerable<IAirportValidator> airportValidators,
        IMapper mapper)
    {
        _flightService = flightService;
        _flightValidators = flightValidators;
        _airportValidators = airportValidators;
        _mapper = mapper;
    }

    [Route("{id:int}")]
    [HttpGet]
    public IActionResult GetFlights(int id)
    {
        var flight = _flightService.GetCompleteFlightById(id);

        if (flight == null)
        {
            return NotFound();
        }

        var response = _mapper.Map<FlightRequest>(flight);
        
        return Ok(response);
    }
    
    [HttpPut]
    public IActionResult AddFlights(FlightRequest request)
    {
        var flight = _mapper.Map<Flight>(request);
        
        if (!_flightValidators.All(f => f.IsValid(flight)) ||
            !_airportValidators.All(a => a.IsValid(flight.From)) ||
            !_airportValidators.All(a => a.IsValid(flight.To)))
        {
            return BadRequest();
        }

        if (_flightService.Exists(flight))
        {
            return Conflict();
        }

        var result = _flightService.Create(flight);

        if (result.Success)
        {
            request = _mapper.Map<FlightRequest>(flight);
            return Created( "", request);
        }

        return Problem(result.FormattedErrors);
    }
    
    [Route("{id:int}")]
    [HttpDelete]
    public IActionResult DeleteFlights(int id)
    {
        var flight = _flightService.GetById(id);
        
        if (flight != null)
        {
           var result = _flightService.Delete(flight);

           if (result.Success)
           {
               return Ok();
           }

           return Problem(result.FormattedErrors);
        }

        return Ok();
    }
}