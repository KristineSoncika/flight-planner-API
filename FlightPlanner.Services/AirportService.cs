using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;

namespace FlightPlanner.Services;

public class AirportService : EntityService<Airport>, IAirportService
{
    public AirportService(IFlightPlannerDbContext context) 
        : base(context) { } 
    
    public List<Airport> FindAirport(string search)
    {
        var formattedPhrase = search.ToLower().Trim();

        var airports = _context.Airports
            .Where(airport => airport.City.ToLower().Contains(formattedPhrase) ||
                              airport.Country.ToLower().Contains(formattedPhrase) ||
                              airport.AirportCode.ToLower().Contains(formattedPhrase));

        return airports.ToList();
    }
}