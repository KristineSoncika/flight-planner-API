using FlightPlanner.Models;

namespace FlightPlanner.Core.Validations;

public class FlightSearchAirportValidator : IFlightSearchValidator
{
    public bool IsValid(FlightSearch flightSearch)
    {
        if (!string.IsNullOrEmpty(flightSearch.From) && !string.IsNullOrEmpty(flightSearch.To))
        {
            return flightSearch.From.Trim().ToLower() != flightSearch.To.Trim().ToLower();
        }

        return false;
    }
}