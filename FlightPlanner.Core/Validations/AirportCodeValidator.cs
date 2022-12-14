using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Validations;

public class AirportCodeValidator : IAirportValidator
{
    public bool IsValid(Airport airport)
    {
        return !string.IsNullOrWhiteSpace(airport.AirportCode);
    }
}