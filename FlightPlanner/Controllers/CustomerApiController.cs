using AutoMapper;
using FlightPlanner.Core.Services;
using FlightPlanner.Core.Validations;
using FlightPlanner.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers;

[Route("api")]
[ApiController]
public class CustomerApiController : ControllerBase
{
    private readonly IFlightService _flightService;
    private readonly IAirportService _airportService;
    private readonly IEnumerable<IFlightSearchValidator> _flightSearchValidators;
    private readonly IMapper _mapper;

    public CustomerApiController(IFlightService flightService,
        IAirportService airportService,
        IEnumerable<IFlightSearchValidator> flightSearchValidators,
        IMapper mapper)
    {
        _flightService = flightService;
        _airportService = airportService;
        _flightSearchValidators = flightSearchValidators;
        _mapper = mapper;
    }
    
    [Route("airports")]
    [HttpGet]
    public IActionResult GetAirport(string search)
    {
        var airports = _airportService.FindAirport(search);
        var response = airports.Select(a => _mapper.Map<AirportRequest>(a));
        
        return Ok(response);
    }
    
    [Route("flights/search")]
    [HttpPost]
    public IActionResult SearchFlight(FlightSearch request)
    {
        if (!_flightSearchValidators.All(f => f.IsValid(request)))
        {
            return BadRequest();
        }
        
        var result = _flightService.SearchFlight(request);

        return Ok(result);
    }
    
    [Route("flights/{id:int}")]
    [HttpGet]
    public IActionResult GetFlight(int id)
    {
        var flight = _flightService.GetCompleteFlightById(id);
        
        if (flight == null)
        {
            return NotFound();
        }
        
        var response = _mapper.Map<FlightRequest>(flight);
        
        return Ok(response);
    }
}