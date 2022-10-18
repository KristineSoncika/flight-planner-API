using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;

namespace FlightPlanner.Services;

public class ClearDb : DbService, IClearDb
{
    public ClearDb(IFlightPlannerDbContext context) 
        : base(context) { }
    
    public void ClearDatabase()
    {
        _context.Flights.RemoveRange(_context.Flights);
        _context.Airports.RemoveRange(_context.Airports);
        _context.SaveChanges();
    }
}