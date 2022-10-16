using FlightPlanner.Models;

namespace FlightPlanner.Core.Validations;

public interface IFlightSearchValidator
{
    bool IsValid(FlightSearch flightSearch);
}