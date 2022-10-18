using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Validations;

public class FlightCarrierValidator : IFlightValidator
{
    public bool IsValid(Flight flight)
    {
        return !string.IsNullOrWhiteSpace(flight.Carrier);
    }
}