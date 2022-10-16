using FlightPlanner.Models;

namespace FlightPlanner.Core.Validations;

public class FlightSearchDepartureDateValidator : IFlightSearchValidator
{
    public bool IsValid(FlightSearch flightSearch)
    {
        return !string.IsNullOrEmpty(flightSearch.DepartureDate);
    }
}