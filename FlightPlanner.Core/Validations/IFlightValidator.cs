using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Validations;

public interface IFlightValidator
{
    bool IsValid(Flight flight);
}