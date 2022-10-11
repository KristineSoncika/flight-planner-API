using FlightPlanner.Context;
using FlightPlanner.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Repositories;

public class FlightRepository
{
    private readonly FlightPlannerDbContext _context;

    public FlightRepository(FlightPlannerDbContext context)
    {
        _context = context;
    }

    public Flight AddFlight(AddFlight request)
    {
        var flight = new Flight
        {
            ArrivalTime = request.ArrivalTime,
            Carrier = request.Carrier,
            DepartureTime = request.DepartureTime,
            From = request.From,
            To = request.To
        };
        _context.Flights.Add(flight);
        _context.SaveChanges();

        return flight;
    }

    public Flight GetFlight(int id)
    {
        var flight = _context.Flights
            .Include(flight => flight.From)
            .Include(flight => flight.To)
            .FirstOrDefault(flight => flight.Id == id);

        return flight;
    }

    public void DeleteFlight(int id)
    {
        var flight = GetFlight(id);
        
        if (flight != null)
        {
            _context.Flights.Remove(flight);
        }
        
        _context.Flights.Remove(flight);
        _context.SaveChanges();
    }

    public bool FlightAlreadyExists(AddFlight request)
    {
        return _context.Flights.Any(f => f.From.City == request.From.City &&
                                         f.From.Country == request.From.Country &&
                                         f.From.AirportCode == request.From.AirportCode &&
                                         f.To.City == request.To.City &&
                                         f.To.Country == request.To.Country &&
                                         f.To.AirportCode == request.To.AirportCode &&
                                         f.Carrier == request.Carrier &&
                                         f.ArrivalTime == request.ArrivalTime &&
                                         f.DepartureTime == request.DepartureTime);
    }

    public void Clear()
    { 
        _context.Flights.RemoveRange(_context.Flights);
        _context.Airports.RemoveRange(_context.Airports);
        _context.SaveChanges();
    }

    public List<Airport> FindAirport(string search)
    {
        var airports = new List<Airport>();
        var formattedPhrase = search.ToLower().Trim();
        
        foreach (var airport in _context.Airports)
        {
            if (airport.City.ToLower().Contains(formattedPhrase) ||
                airport.Country.ToLower().Contains(formattedPhrase) ||
                airport.AirportCode.ToLower().Contains(formattedPhrase))
            {
                airports.Add(airport);
                return airports.ToList();
            }
            if (airport.City.ToLower().Contains(formattedPhrase) ||
                airport.Country.ToLower().Contains(formattedPhrase) ||
                airport.AirportCode.ToLower().Contains(formattedPhrase))
            {
                airports.Add(airport);
                return airports.ToList();
            }
        }
        
        return airports.ToList();
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