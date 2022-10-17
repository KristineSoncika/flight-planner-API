using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using FlightPlanner.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Services;

public class FlightService: EntityService<Flight>, IFlightService
{
    public FlightService(IFlightPlannerDbContext context) 
        : base(context) { }
    
    public Flight GetCompleteFlightById(int id)
    {
        return _context.Flights
            .Include(f => f.From)
            .Include(f => f.To)
            .SingleOrDefault(f => f.Id == id);
    }

    public bool Exists(Flight flight)
    {
       return _context.Flights.Any(f => f.ArrivalTime == flight.ArrivalTime &&
                                  f.DepartureTime == flight.DepartureTime &&
                                  f.Carrier == flight.Carrier &&
                                  f.From.AirportCode == flight.From.AirportCode &&
                                  f.To.AirportCode == flight.To.AirportCode);
    }

    public PageResult SearchFlight(FlightSearch search)
     {
         var result = new PageResult();
         var totalItems = 0;
         var items = new List<Flight>();
         
         var flight = _context.Flights
             .Include(flight => flight.From)
             .Include(flight => flight.To)
             .FirstOrDefault(flight => flight.DepartureTime.Contains(search.DepartureDate) &&
                                       flight.From.AirportCode == search.From &&
                                       flight.To.AirportCode == search.To);

         if (flight != null)
         {
             items.Add(flight);
             totalItems++;
         }

         result.Items = items;
         result.TotalItems = totalItems;
         return result;
     }
}